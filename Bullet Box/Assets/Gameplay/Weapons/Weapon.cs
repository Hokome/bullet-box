using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class Weapon : MonoBehaviour, IShooter
    {
		[SerializeField] private string id;
		[SerializeField] private ProjectilePattern pattern;

		private SpriteRenderer sr;
		public SpriteRenderer Sr
		{
			get
			{
				if (sr == null)
					sr = GetComponent<SpriteRenderer>();
				return sr;
			}
		}

		public string ID => id;

		private float lastShot;

		public bool CanShoot => true;
		public Transform Transform => transform;
		public float AttackSpeed => 1f;
		public MonoBehaviour Behaviour => this;

		public void Fire()
		{
			if (Time.time - lastShot >= AttackSpeed / pattern.FireRate)
			{
				StartCoroutine(pattern.ShootOnce(this));
				lastShot = Time.time;
			}
		}
	}
}
