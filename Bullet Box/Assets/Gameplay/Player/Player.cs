using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

using InputCallback = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace BulletBox
{
	[RequireComponent(typeof(PlayerInput))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class Player : MonoSingleton<Player>, IHittable
	{
		[SerializeField] private float acceleration = 1f;
		[SerializeField] private float maxSpeed = 1f;
		[Space]
		[SerializeField] private int maxHealth = 3;
		[SerializeField] private float invincibilityTime = 2f;
		[Space]
		[SerializeField] private Ability ability;
		[Header("Cheats")]
		[SerializeField] private bool godMode;
		[SerializeField] private float timeScale;
		public float TimeScale => timeScale;

		[HideInInspector] public Weapon[] weapons;

		private float lastAbility = float.NegativeInfinity;

		private Camera cam;
		private PlayerInput input;
		private SpriteRenderer sr;

		#region Auto Props
		public Vector2 MoveInput { get; private set; }
		public Vector2 AimInput { get; private set; }
		public bool FireInput { get; private set; }
		public Rigidbody2D Rb { get; private set; }
		#endregion

		private int health;
		public int Health
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
				SetSpriteInvincible(Weapon.Sr, false);
				weapons[weaponIndex].gameObject.SetActive(false);
				HUDManager.Inst.WeaponHUDs[weaponIndex].Selected = false;

				if (value < 0)
					value += weapons.Length;
				weaponIndex = value % weapons.Length;


				SetSpriteInvincible(Weapon.Sr, Invincible);
				Weapon.gameObject.SetActive(true);
				HUDManager.Inst.WeaponHUDs[weaponIndex].Selected = true;

				if (FireInput)
					Weapon.OnFireDown();
			}
		}
		public Weapon Weapon
		{
			get => weapons[weaponIndex];
			set
			{
				Destroy(Weapon.gameObject);
				weapons[WeaponIndex] = Instantiate(value, transform);
				HUDManager.Inst.WeaponHUDs[weaponIndex].Weapon = Weapon;
				WeaponIndex = WeaponIndex;
			}
		}

		private SpecialWeapon special;
		public SpecialWeapon Special
		{
			get => special;
			set
			{
				if (special != null)
					Destroy(special.gameObject);
				special = Instantiate(value, transform);
				HUDManager.Inst.SpecialHUD.Special = special;
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
				SetSpriteInvincible(Weapon.Sr, value);
			}
		}


		#region Messages
		protected override void Awake()
		{
			base.Awake();
			input = GetComponent<PlayerInput>();
			InitializeInput();
		}
		private void Start()
		{
			Rb = GetComponent<Rigidbody2D>();
			sr = GetComponentInChildren<SpriteRenderer>();
			cam = Camera.main;
			Time.timeScale = TimeScale;

			HUDManager.Inst.HealthBar.Max = maxHealth;
			Health = maxHealth;

			HUDManager.Inst.SpecialHUD.Special = null;
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
			if (Time.timeScale <= 0f) return;
			Vector2 direction;
			if (input.currentControlScheme != "Gamepad")
			{
				Vector3 point = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
				//Using another variable to cast to Vector2
				direction = point - Weapon.transform.position;
				AimInput = direction.normalized;
				Weapon.Crosshair.transform.position = (Vector2)point;
			}
			else
			{
				direction = AimInput;
				Weapon.Crosshair.transform.position
					= (Vector2)transform.position + (AimInput * Weapon.Crosshair.Distance);
			}
			if (AimInput.x > 0f)
			{
				sr.flipX = false;
				Weapon.Sr.flipY = false;
			}
			else
			{
				sr.flipX = true;
				Weapon.Sr.flipY = true;
			}
			Weapon.transform.right = direction;
		}
		#endregion

		#region Input
		private InputAction moveAction;
		private InputAction aimAction;
		private InputAction fireAction;
		private InputAction abilityAction;
		private InputAction scrollAction;
		private InputAction specialAction;
		private InputAction pauseAction;

		private delegate void InputHandler(InputCallback ctx);
		private Dictionary<InputAction, InputHandler> actionHandlers;

		private void InitializeInput()
		{
			actionHandlers = new Dictionary<InputAction, InputHandler>(8);

			moveAction = input.actions.FindAction("Move");
			actionHandlers.Add(moveAction, Move);

			aimAction = input.actions.FindAction("Aim");
			actionHandlers.Add(aimAction, Aim);

			fireAction = input.actions.FindAction("Fire");
			actionHandlers.Add(fireAction, Fire);

			abilityAction = input.actions.FindAction("Ability");
			actionHandlers.Add(abilityAction, UseAbility);

			scrollAction = input.actions.FindAction("Scroll");
			actionHandlers.Add(scrollAction, Scroll);

			specialAction = input.actions.FindAction("Special");
			actionHandlers.Add(specialAction, UseSpecial);

			pauseAction = input.actions.FindAction("Pause");
			actionHandlers.Add(pauseAction, Pause);
		}
		private void ReadInput(InputCallback ctx)
		{
			if (actionHandlers.TryGetValue(ctx.action, out InputHandler ih))
				ih(ctx);
		}
		private void Move(InputCallback ctx)
		{
			MoveInput = ctx.ReadValue<Vector2>();
			MoveInput = Vector2.ClampMagnitude(MoveInput, 1f);
		}
		private void Aim(InputCallback ctx)
		{
			Vector2 v = ctx.ReadValue<Vector2>();
			if (v == Vector2.zero) return;
			AimInput = v.normalized;
		}
		private void Fire(InputCallback ctx)
		{
			if (ctx.performed)
			{
				Weapon.OnFireDown();
				FireInput = true;

				HasShot = true;
			}
			else if (ctx.canceled)
			{
				FireInput = false;
				Weapon.OnFireUp();
			}
		}
		private void UseAbility(InputCallback ctx)
		{
			if (!ctx.performed) return;
			if (Time.time - lastAbility >= ability.Cooldown)
			{
				if (ability.Use(this))
				{
					HasUsedAbility = true;
					HUDManager.Inst.SetAbilityCooldown(ability.Cooldown);
					lastAbility = Time.time;
				}
			}
		}
		private float lastScroll = float.NegativeInfinity;
		private void Scroll(InputCallback ctx)
		{
			if (!ctx.performed) return;
			if (Time.time - lastScroll < 0.1f)
				return;
			int delta = (int)Mathf.Sign(ctx.ReadValue<float>());
			WeaponIndex += delta;
			lastScroll = Time.time;
		}
		private void UseSpecial(InputCallback ctx)
		{
			if (!ctx.performed) return;
			if (special == null || special.InUse) return;

			HasUsedSpecial = true;
			special.Use();
		}
		private void Pause(InputCallback ctx)
		{
			if (!ctx.performed) return;
			PauseMenu.Inst.TogglePause();
		}
		#endregion

		#region Tutorial check
		public bool HasShot { get; private set; }
		public bool HasUsedAbility { get; private set; }
		public bool HasUsedSpecial { get; private set; }
		#endregion
		public bool HasWeapon(Weapon w) => weapons.Any(o => o.ID == w.ID);
		public void SetLoadout(WeaponLoadout loadout)
		{
			weapons = new Weapon[loadout.weapons.Length];
			for (int i = 0; i < loadout.weapons.Length; i++)
			{
				SetWeapon(i, loadout.weapons[i]);
			}
			WeaponIndex = 0;
		}
		public void SetWeapon(int index, Weapon weaponChoice)
		{
			if (weapons[index] != null)
				Destroy(weapons[index].gameObject);
			weaponChoice = Instantiate(weaponChoice, transform);
			weapons[index] = weaponChoice;
			HUDManager.Inst.WeaponHUDs[index].Weapon = weaponChoice;
			WeaponIndex = index;
		}

		public void Hit(float damage)
		{
			if (godMode) return;
			if (Invincible)
				return;
			Health--;

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