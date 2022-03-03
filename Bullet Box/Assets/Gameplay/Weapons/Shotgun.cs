using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class Shotgun : FiringWeapon
    {
		[SerializeField] private int shotCount;
		[SerializeField] private float shotAngle;
		[SerializeField] private float innacuracy;
		[SerializeField] private float damage;
		public override bool Fire()
		{
			bool b = base.Fire();
			if (b)
			{
				float startAngle = -(shotAngle * 0.5f);
				float angle = shotAngle / shotCount;
				for (int i = 0; i < shotCount; i++)
				{
					Projectile p = CreateProjectile();
					p.transform.Rotate(Vector3.forward, startAngle + angle * i + Random.Range(-innacuracy, innacuracy));
					p.damage = damage;
				}
				return true;
			}
			return false;
		}
	}
}
