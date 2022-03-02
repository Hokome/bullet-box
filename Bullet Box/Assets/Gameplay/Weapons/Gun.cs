using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	public class Gun : Weapon
	{
		[SerializeField] private float fireRate;
		[SerializeField] private float damage;
		[SerializeField] private float offset;
		[SerializeField] private Projectile projectile;

		private float lastShot = float.NegativeInfinity;

		public override bool Fire()
		{
			if (Time.time - lastShot >= 1f / fireRate)
			{
				lastShot = Time.time;
				Projectile p = Instantiate(projectile);
				p.transform.SetPositionAndRotation(
					transform.position + transform.right * offset,
					transform.rotation);
				p.damage = damage;
				return true;
			}
			return false;
		}
	}
}
