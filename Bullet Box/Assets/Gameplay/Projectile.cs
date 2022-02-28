using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	public class Projectile : MonoBehaviour
	{
		[SerializeField] private float speed;
		[SerializeField] private LayerMask hitLayer;
		[SerializeField] private LayerMask obstacleLayer;

		[HideInInspector] public float damage;

		private void Start()
		{
			GetComponent<Rigidbody2D>().velocity = transform.right * speed;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (Utility.IsInLayerMask(collision.gameObject.layer, hitLayer))
			{
				collision.GetComponent<IHittable>().Hit(damage);
				Destroy(gameObject);
			}
			else if (Utility.IsInLayerMask(collision.gameObject.layer, obstacleLayer))
			{
				Destroy(gameObject);
			}
		}
	}
}
