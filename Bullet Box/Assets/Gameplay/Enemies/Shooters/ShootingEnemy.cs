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
		[SerializeField] private float rotationSpeed;
		[Space]
		[SerializeField] private Transform weaponTransform;
		[SerializeField] private AimType aimType;
		[SerializeField] private ProjectilePattern projectilePattern;
		[SerializeField] private float projectileDamage;
		[SerializeField] private float projectileSpeed;

		public bool CanShoot => true;
		public float AttackSpeed => 1f;
		public Transform Transform => weaponTransform;
		public MonoBehaviour Behaviour => this;

		private void Awake()
		{

			projectilePattern.projectile = Instantiate(projectilePattern.projectile, transform);
			projectilePattern.projectile.damage = projectileDamage;
			projectilePattern.projectile.speed = projectileSpeed;

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
	}
}
