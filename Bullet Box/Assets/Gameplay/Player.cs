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

		[SerializeField] private Transform gunTransform;
		[SerializeField] private Gun weapon;

		private bool invincible = false;

		private Rigidbody2D rb;
		private Camera cam;
		private PlayerInput input;

		private Vector2 moveInput;
		private bool fireInput;

		private float health;
		public float Health
		{
			get => health;
			set
			{
				health = value;
				HUDManager.Inst.HealthBar.Value = value;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			input = GetComponent<PlayerInput>();
		}

		private void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			cam = Camera.main;

			HUDManager.Inst.HealthBar.Max = maxHealth;
			Health = maxHealth;
			HUDManager.Inst.HealthBar.ForceUpdate();

			moveAction = input.actions.FindAction("Move");
			fireAction = input.actions.FindAction("Fire");
		}

		private void Update()
		{
			Vector3 point = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
			//Using another variable to cast to Vector2
			Vector2 direction = point - gunTransform.transform.position;
			gunTransform.transform.right = direction;
			if (fireInput)
				weapon.Fire();
		}
		private void FixedUpdate()
		{
			float force = acceleration * Time.fixedDeltaTime;

			if (Vector2.Dot(rb.velocity, moveInput) < 0f
				|| Vector3.Project(rb.velocity, moveInput).sqrMagnitude < maxSpeed * maxSpeed)
				rb.velocity += moveInput * force;
		}
		private void OnEnable()
		{
			input.onActionTriggered += ReadInput;
		}
		private void OnDisable()
		{
			input.onActionTriggered -= ReadInput;
		}

		private InputAction moveAction;
		private InputAction fireAction;

		private void ReadInput(InputCallback ctx)
		{
			if (ctx.action == moveAction)
				Move(ctx);
			else if (ctx.action == fireAction)
				Fire(ctx);
		}
		private void Move(InputCallback ctx)
		{
			moveInput = ctx.ReadValue<Vector2>();
			moveInput = Vector2.ClampMagnitude(moveInput, 1f);
		}

		private void Fire(InputCallback ctx)
		{
			if (ctx.performed)
				fireInput = true;
			else if (ctx.canceled)
				fireInput = false;
		}

		public void Hit(float damage)
		{
			if (invincible)
				return;
			Health -= damage;

			invincible = true;
			LeanTween.delayedCall(invincibilityTime, () => invincible = false);

			if (Health <= 0f)
				Die();
		}
		private void Die()
		{
			Destroy(gameObject);
		}
	}
}