using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class ExplosiveProjectile : Projectile
    {
		[SerializeField] private float splashDamage;
		[SerializeField] private float explosionRadius;
		[SerializeField] private ParticleSystem explosionParticles;

		private bool exploded = false;

		protected override void Hit(IHittable hit)
		{
			Explode();
			base.Hit(hit);
		}
		protected override void Obstacle()
		{
			Explode();
			base.Obstacle();
		}
		private void Explode()
		{
			if (exploded) return;
			ParticleSystem ps = Instantiate(explosionParticles);
			ps.transform.position = transform.position;
			var main = ps.main;
			main.startSize = explosionRadius * 2f;

			Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, hitLayer);
			foreach (Collider2D c in hits)
			{
				c.GetComponent<IHittable>().Hit(splashDamage);
			}
			exploded = true;
		}
	}
}
