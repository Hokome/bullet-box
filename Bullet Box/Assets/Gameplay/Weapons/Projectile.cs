using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	public class Projectile : MonoBehaviour
	{
		[SerializeField] private float speed;
		[SerializeField] private int pierce = 1;
		[SerializeField] protected LayerMask hitLayer;
		[SerializeField] protected LayerMask obstacleLayer;

		[HideInInspector] public float damage;

		private List<IHittable> hits;

		private void Start()
		{
			if (pierce > 1)
				hits = new List<IHittable>(pierce);
			GetComponent<Rigidbody2D>().velocity = transform.right * speed;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (Utility.IsInLayerMask(collision.gameObject.layer, hitLayer))
			{
				IHittable hit = collision.GetComponent<IHittable>();
				Hit(hit);
			}
			else if (Utility.IsInLayerMask(collision.gameObject.layer, obstacleLayer))
			{
			}
		}
		protected virtual void Hit(IHittable hit)
		{
			if (hits != null)
			{
				if (hits.Contains(hit))
					return;
				else
					hits.Add(hit);
			}
			hit.Hit(damage);
			pierce--;
			if (pierce <= 0)
				Destroy(gameObject);
		}
		protected virtual void Obstacle()
		{
			Destroy(gameObject);
		}
	}
}
