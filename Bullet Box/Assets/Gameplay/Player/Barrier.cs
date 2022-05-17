using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	public class Barrier : MonoBehaviour, IHittable
	{
		[SerializeField] private float health;
		[SerializeField] private float duration;
		[SerializeField] private float deploySpeed;
		[SerializeField] private float deployTime;

		private Rigidbody2D rb;

		public void Deploy(Vector2 velocity, float time)
		{
			rb = GetComponent<Rigidbody2D>();
			rb.velocity = velocity;
			this.DelayCall(Fasten, time);
			Destroy(gameObject, duration);
		}
		private void Fasten()
		{
			rb.velocity = Vector2.zero;
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
		}

		public void Hit(float damage)
		{
			health -= damage;
			if (health <= 0f)
				Destroy(gameObject);
		}
	}
}
