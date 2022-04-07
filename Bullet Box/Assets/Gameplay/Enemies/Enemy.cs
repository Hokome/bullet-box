using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Collider2D))]
	public abstract class Enemy : Spawnable, IHittable
    {
		[SerializeField] private SpriteRenderer flash;
		[Header("Stats")]
		[SerializeField] protected float maxHealth;
		[SerializeField] private int experience;

		protected Rigidbody2D rb;
		public float Health { get; set; }

		private bool isDead;

		protected virtual void Start()
		{
			rb = GetComponent<Rigidbody2D>();
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
				StartCoroutine(Flash());
		}
		private IEnumerator Flash()
		{
			flash.enabled = true;
			yield return new WaitForEndOfFrame();
			yield return new WaitForSeconds(0.02f);
			flash.enabled = false;
		}
		public virtual void Kill()
		{
			if (isDead) return;
			isDead = true;
			Destroy(gameObject);
			EnemySpawner.Inst.Experience(experience, transform.position);
		}
	}
}
