using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


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

		[SerializeField] private InputActionReference fireAction;
		[SerializeField] private Transform gunTransform;
		[SerializeField] private Gun weapon;

		private bool invincible = false;

		private Rigidbody2D rb;
		private Camera cam;

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

		private void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			cam = Camera.main;
			HUDManager.Inst.HealthBar.Max = maxHealth;
			Health = maxHealth;
			HUDManager.Inst.HealthBar.ForceUpdate();
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

		private void OnMove(InputValue iv)
		{
			moveInput = iv.Get<Vector2>();
			moveInput = Vector2.ClampMagnitude(moveInput, 1f);
		}

		private void OnEnable()
		{
			fireAction.action.performed += _ => fireInput = true;
			fireAction.action.canceled += _ => fireInput = false;
		}
		private void OnDisable()
		{
			fireAction.action.performed -= _ => fireInput = true;
			fireAction.action.canceled -= _ => fireInput = false;
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