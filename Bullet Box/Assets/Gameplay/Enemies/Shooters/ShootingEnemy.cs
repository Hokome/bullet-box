using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	public enum AimType
	{
		Static, //No aiming
		ConstantRotation, //Rotates at a constant speed
		Aim //Aims towards the player
	}
    public class ShootingEnemy : Enemy, IShooter
    {
		[SerializeField] private float acceleration;
		[SerializeField] private float maxSpeed;
		[SerializeField] private float minDist;
		[SerializeField] private float maxDist;
		[SerializeField] private float rotationSpeed;
		[Space]
		[SerializeField] private Transform weaponTransform;
		[SerializeField] private AimType aimType;
		[SerializeField] private ProjectilePattern projectilePattern;

		private float c_sqrMinDist;
		private float c_sqrMaxDist;

		public bool CanShoot => true;
		public float AttackSpeed => 1f;
		public Transform Transform => weaponTransform;
		public MonoBehaviour Behaviour => this;

		private void Awake()
		{
			c_sqrMinDist = minDist * minDist;
			c_sqrMaxDist = maxDist * maxDist;
			StartCoroutine(projectilePattern.StartPattern(this));
		}

		private void Update()
		{
			switch (aimType)
			{
				case AimType.Static:
					break;
				case AimType.ConstantRotation:
					weaponTransform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
					break;
				case AimType.Aim:
					Vector2 v = Player.Inst.transform.position - transform.position;
					weaponTransform.right = v;
					break;
			}
		}

		private void FixedUpdate()
		{
			Vector2 v = Player.Inst.transform.position - transform.position;
			if (v.sqrMagnitude < c_sqrMinDist)
			{
				Vector2 vn = -v.normalized;
				if (MathEx.MaxSpeed(rb.velocity, vn * maxSpeed))
					rb.velocity += acceleration * Time.fixedDeltaTime * vn;
			}
			else if (v.sqrMagnitude > c_sqrMaxDist)
			{
				Vector2 vn = v.normalized;
				if (MathEx.MaxSpeed(rb.velocity, vn * maxSpeed))
					rb.velocity += acceleration * Time.fixedDeltaTime * vn;
			}
		}
	}
}
