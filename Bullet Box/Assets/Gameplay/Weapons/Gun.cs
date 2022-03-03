using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	public class Gun : FiringWeapon
	{
		[SerializeField] private float damage;

		public override bool Fire()
		{
			bool b = base.Fire();
			if (b)
				CreateProjectile().damage = damage;
			return b;
		}
	}
}
