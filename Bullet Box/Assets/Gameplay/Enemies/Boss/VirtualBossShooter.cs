using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class VirtualBossShooter : MonoBehaviour, IShooter
    {
		[SerializeField] private float rotationSpeed;
		[SerializeField] private float damage;
		[SerializeField] private float speed;
		[SerializeField] private ProjectilePattern pattern;

		private Boss boss;
		
		#region IShooter
		public bool CanShoot => boss.CanShoot;
		public Transform Transform => transform;
		public float AttackSpeed => boss.AttackSpeed;
		public MonoBehaviour Behaviour => this;
		#endregion

		private void Start()
		{
			boss = GetComponentInParent<Boss>();
			pattern.projectile = Instantiate(pattern.projectile, transform);
			pattern.projectile.damage = damage;
			pattern.projectile.speed = speed;

			StartCoroutine(pattern.StartPattern(this));
		}

		private void Update()
		{
			transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
		}
	}
}
