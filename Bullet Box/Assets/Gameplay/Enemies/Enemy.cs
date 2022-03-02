using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	[RequireComponent(typeof(Rigidbody2D))]
	public abstract class Enemy : Spawnable, IHittable
    {
		[SerializeField] private SpriteRenderer flash;
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
			Destroy(gameObject);
		}
	}
}
