using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	public class Projectile : MonoBehaviour
	{
		[HideInInspector] public float damage;
		[HideInInspector] public float speed;
		[HideInInspector] public float range;
		[HideInInspector] public int pierce = 1;
		[SerializeField] protected LayerMask hitLayer;
		[SerializeField] protected LayerMask obstacleLayer;

		//Used to avoid hitting the same object twice
		private List<IHittable> hits;

		private void Start()
		{
			if (pierce > 1)
				hits = new List<IHittable>(pierce);
			GetComponent<Rigidbody2D>().velocity = transform.right * speed;
		}

		private void OnEnable()
		{
			if (range > 0)
				Destroy(gameObject, range / speed);
		}

		public virtual void SetLevel(int level, CSVReader table)
		{
			damage = table.ReadFloat("Damage", level);
			speed = table.ReadFloat("Speed", level);
			range = table.ReadFloat("Range", level);
			pierce = table.ReadInt("Pierce", level);
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
				Obstacle();
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
