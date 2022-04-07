using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class ExplosiveProjectile : Projectile
    {
		[SerializeField] private ParticleSystem explosionParticles;
		
		[HideInInspector] public float splashDamage;
		[HideInInspector] public float explosionRadius;

		//Prevents multiple explosions if the projectile enters collision with multiple objects at once.
		private bool exploded = false;

		public override void SetLevel(int level, CSVReader table)
		{
			base.SetLevel(level, table);
			explosionRadius = table.ReadFloat("E Radius", level);
			splashDamage = table.ReadFloat("E Damage", level);
		}
		protected override void Hit(IHittable hit)
		{
			Explode();
			base.Hit(hit);
		}
		private void OnDestroy()
		{
			Explode();
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
