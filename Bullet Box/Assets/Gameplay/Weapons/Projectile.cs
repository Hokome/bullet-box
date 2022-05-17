using BulletBox.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	public class Projectile : MonoBehaviour
	{
		public float damage;
		public float speed;
		public float acceleration = 0f;
		public float range;

		[SerializeField] protected LayerMask hitLayer;
		[SerializeField] protected LayerMask obstacleLayer;
		[SerializeField] protected SFXClip hitSound;
		[SerializeField] protected SFXClip obstacleSound;
		[SerializeField] protected ParticleSystem hitParticles;

		private Rigidbody2D rb;
		public Rigidbody2D Rb
		{
			get
			{
				if (rb == null)
					rb = GetComponent<Rigidbody2D>();
				return rb;
			}
			protected set => rb = value;
		}

		protected virtual void Start()
		{
			Rb.velocity = transform.right * speed;
		}

		private void OnEnable()
		{
			if (range > 0)
				Destroy(gameObject, range / speed);
		}

		private void FixedUpdate()
		{
			Rb.velocity += acceleration * Time.fixedDeltaTime * (Vector2)transform.right;
		}

		//public virtual void SetLevel(int level, CSVReader table)
		//{
		//	damage = table.ReadFloat("Damage", level);
		//	speed = table.ReadFloat("Speed", level);
		//	range = table.ReadFloat("Range", level);
		//	pierce = table.ReadInt("Pierce", level);
		//}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (Utility.IsInLayerMask(collision.gameObject.layer, hitLayer))
			{
				IHittable hit = collision.gameObject.GetComponent<IHittable>();
				Hit(hit, collision);
			}
			else if (Utility.IsInLayerMask(collision.gameObject.layer, obstacleLayer))
			{
				Obstacle(collision);
			}
		}
		
		protected virtual void Hit(IHittable hit, Collision2D c)
		{
			hit.Hit(damage);
			if (hitSound != null)
				AudioManager.PlaySound(hitSound, transform.position);
			HitParticles(c);
			Destroy(gameObject);
		}
		protected virtual void Obstacle(Collision2D c)
		{
			if (obstacleSound != null) 
				AudioManager.PlaySound(obstacleSound, transform.position);
			HitParticles(c);
			Destroy(gameObject);
		}
		protected void HitParticles(Collision2D c)
		{
			if (hitParticles == null) return;
			ParticleSystem ps = Instantiate(hitParticles, transform.position, transform.rotation);
			Vector2 normal = -c.GetContact(0).normal;
			ps.transform.right = MathEx.Average(normal, transform.right);
		}
	}
}
