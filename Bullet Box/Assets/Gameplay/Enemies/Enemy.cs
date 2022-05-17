using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Collider2D))]
	[RequireComponent(typeof(SpriteRenderer))]
	public abstract class Enemy : Spawnable, IHittable
    {
		[Header("Stats")]
		[SerializeField] protected float maxHealth;

		private HitFlash flash;
		protected Rigidbody2D rb;
		public float Health { get; set; }

		private bool isDead;

		protected virtual void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			flash = GetComponent<HitFlash>();

			if (GameManager.GameMode == GameMode.Arcade)
				LevelSpawner.Inst.NotifySpawn();
		}

		public virtual void ScaleDifficulty(float difficulty)
		{
			maxHealth *= difficulty;
			Health = maxHealth;
		}
		public virtual void Hit(float damage)
		{
			Health -= damage;
			if (Health <= 0f)
				Kill();
			else
				flash.Flash();
		}
		public virtual void Kill()
		{
			if (isDead) return;
			isDead = true;
			Destroy(gameObject);
		}
	}
}
