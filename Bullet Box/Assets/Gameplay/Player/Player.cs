using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using InputCallback = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace BulletBox
{
	[RequireComponent(typeof(PlayerInput))]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(CircleCollider2D))]
	public class Player : MonoSingleton<Player>, IHittable
	{
		[SerializeField] private float acceleration = 1f;
		[SerializeField] private float maxSpeed = 1f;
		[SerializeField] private float maxHealth = 5f;
		[SerializeField] private float invincibilityTime = 3f;

		[SerializeField] private List<Weapon> weapons;
		[SerializeField] private Ability ability;

		private float lastAbility = float.NegativeInfinity;
		private Weapon weapon;
		private Pickupable pickupable;

		private Camera cam;
		private PlayerInput input;
		private SpriteRenderer sr;

		public Vector2 MoveInput { get; private set; }
		public bool FireInput { get; private set; }
		public Rigidbody2D Rb { get; private set; }

		private float health;
		public float Health
		{
			get => health;
			set
			{
				health = Mathf.Min(value, maxHealth);
				HUDManager.Inst.HealthBar.Value = health;
			}
		}

		private int weaponIndex;
		private int WeaponIndex
		{
			get => weaponIndex;
			set
			{
				SetSpriteInvincible(weapon.Sr, false);
				weapons[weaponIndex].gameObject.SetActive(false);
				HUDManager.Inst.WeaponHUDs[weaponIndex].Selected = false;

				if (value < 0)
					value += weapons.Count;
				weaponIndex = value % weapons.Count;

				weapon = weapons[weaponIndex];
				SetSpriteInvincible(weapon.Sr, Invincible);
				weapon.gameObject.SetActive(true);
				HUDManager.Inst.WeaponHUDs[weaponIndex].Selected = true;
			}
		}
		public Weapon Weapon
		{
			get => weapon;
			set
			{
				Destroy(weapon.gameObject);
				weapons[WeaponIndex] = Instantiate(value, transform);
				WeaponIndex = WeaponIndex;
			}
		}
		private bool invincible;
		private bool Invincible
		{
			get => invincible;
			set
			{
				invincible = value;
				SetSpriteInvincible(sr, value);
				SetSpriteInvincible(weapon.Sr, value);
			}
		}

		#region Messages
		protected override void Awake()
		{
			base.Awake();
			input = GetComponent<PlayerInput>();
		}
		private void Start()
		{
			Rb = GetComponent<Rigidbody2D>();
			sr = GetComponent<SpriteRenderer>();
			cam = Camera.main;

			HUDManager.Inst.HealthBar.Max = maxHealth;
			Health = maxHealth;
			HUDManager.Inst.HealthBar.ForceUpdate();

			weapon = weapons[0];
			for (int i = 0; i < weapons.Capacity; i++)
			{
				if (weapons.Count <= i)
				{
					HUDManager.Inst.WeaponHUDs[i].Weapon = null;
					break;
				}
				weapons[i] = Instantiate(weapons[i], transform);
				HUDManager.Inst.WeaponHUDs[i].Weapon = weapons[i];

			}

			WeaponIndex = 1;
			WeaponIndex = 0;

			moveAction = input.actions.FindAction("Move");
			fireAction = input.actions.FindAction("Fire");
			abilityAction = input.actions.FindAction("Ability");
			scrollAction = input.actions.FindAction("Scroll");
			pickupAction = input.actions.FindAction("Pickup");
		}
		private void OnEnable()
		{
			input.onActionTriggered += ReadInput;
		}
		private void OnDisable()
		{
			input.onActionTriggered -= ReadInput;
		}
		private void FixedUpdate()
		{
			float force = acceleration * Time.fixedDeltaTime;

			if (MathEx.MaxSpeed(Rb.velocity, MoveInput * maxSpeed))
				Rb.velocity += MoveInput * force;
		}
		private void Update()
		{
			Vector3 point = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
			//Using another variable to cast to Vector2
			Vector2 direction = point - weapon.transform.position;
			weapon.transform.right = direction;
			if (FireInput)
				weapon.Fire();
		}
		#endregion

		#region Input
		private InputAction moveAction;
		private InputAction fireAction;
		private InputAction abilityAction;
		private InputAction scrollAction;
		private InputAction pickupAction;

		private float lastScroll = float.NegativeInfinity;

		private void ReadInput(InputCallback ctx)
		{
			if (ctx.action == moveAction)
				Move(ctx);
			else if (ctx.action == fireAction)
				Fire(ctx);
			else if (ctx.action == abilityAction)
				UseAbility(ctx);
			else if (ctx.action == scrollAction)
				Scroll(ctx);
			else if (ctx.action == pickupAction)
				PickUp(ctx);
		}
		private void Move(InputCallback ctx)
		{
			MoveInput = ctx.ReadValue<Vector2>();
			MoveInput = Vector2.ClampMagnitude(MoveInput, 1f);
		}
		private void Fire(InputCallback ctx)
		{
			if (ctx.performed)
				FireInput = true;
			else if (ctx.canceled)
				FireInput = false;
		}
		private void UseAbility(InputCallback ctx)
		{
			if (!ctx.performed) return;
			if (Time.time - lastAbility >= ability.Cooldown)
			{
				if (ability.Use(this))
				{
					HUDManager.Inst.SetAbilityCooldown(ability.Cooldown);
					lastAbility = Time.time;
				}
			}
		}
		private void Scroll(InputCallback ctx)
		{
			if (Time.time - lastScroll < 0.1f)
				return;
			int delta = (int)Mathf.Sign(ctx.ReadValue<float>());
			WeaponIndex += delta;
			lastScroll = Time.time;
		}
		private void PickUp(InputCallback ctx)
		{
			if (!ctx.performed) return;
			if (pickupable == null) return;

			pickupable.PickUp(this);
			pickupable = null;
		}
		#endregion

		public void NotifyPickup(Pickupable p, bool enter)
		{
			if (enter)
				pickupable = p;
			else
			{
				if (p == null)
					pickupable = p;
			}
		}
		public void Hit(float damage)
		{
			if (Invincible)
				return;
			Health -= damage;

			StartCoroutine(HitCoroutine());

			if (Health <= 0f)
				Die();
		}
		private IEnumerator HitCoroutine()
		{
			Invincible = true;
			yield return new WaitForSeconds(invincibilityTime);
			Invincible = false;
		}
		private void Die()
		{
			GameManager.Inst.GameOver();
		}

		private void SetSpriteInvincible(SpriteRenderer sr, bool v)
		{
			sr.color = Utility.ChangeAlpha(sr.color, v ? 0.5f : 1f);
		}
	}
}