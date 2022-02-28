using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	[RequireComponent(typeof(Rigidbody2D))]
	public abstract class Enemy : MonoBehaviour, IHittable
    {
		[Header("Spawning")]
		[SerializeField] private float spawnCooldown;
		[SerializeField] private float spawnCost;
		[Header("Stats")]
		[SerializeField] private float maxHealth;

		protected Rigidbody2D rb;

		public float Health { get; set; }

		protected virtual void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			Health = maxHealth;
		}

		public virtual void Hit(float damage)
		{
			Health -= damage;
			if (Health <= 0f)
				Kill();
		}
		public void Kill()
		{
			Destroy(gameObject);
		}
	}
}
