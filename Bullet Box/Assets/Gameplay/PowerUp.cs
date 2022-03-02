using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	[RequireComponent(typeof(CircleCollider2D))]
	public abstract class PowerUp : Spawnable
    {
		[SerializeField] private float maxLifeTime = 30f;

		private void Start()
		{
			Destroy(gameObject, maxLifeTime);
		}

		public abstract void OnPickUp(Player p);

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.CompareTag("Player"))
			{
				OnPickUp(collision.GetComponent<Player>());
				Destroy(gameObject);
			}
		}
	}
}
