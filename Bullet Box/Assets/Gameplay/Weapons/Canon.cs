using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	public class Canon : FiringWeapon
	{
		[SerializeField] private float damage;
		[SerializeField] private float splashDamage;
		public override bool Fire()
		{
			bool b = base.Fire();
			if (b)
			{
				CanonProjectile c = (CanonProjectile)CreateProjectile();
				c.damage = damage;
				c.splashDamage = splashDamage;
			}
			return b;
		}
	}
}
