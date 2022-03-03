using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class BulletStorm : SpecialWeapon
    {
		[SerializeField] private float damage;
		[SerializeField] private int bulletCount;
		[SerializeField] private int totalShots;
		[SerializeField] private float shotsInterval;
		[SerializeField] private Projectile projectile;

		public override void Use()
		{
			InUse = true;
			Uses--;
			StartCoroutine(ShotsCoroutine());
		}

		private IEnumerator ShotsCoroutine()
		{
			float angle = 360f / bulletCount;
			for (int i = 0; i < totalShots; i++)
			{
				for (int j = 0; j < bulletCount; j++)
				{
					Projectile p = Instantiate(projectile);
					p.transform.SetPositionAndRotation(
						transform.position,
						Quaternion.Euler(0f, 0f, angle * j)
						);
					p.damage = damage;
				}
				yield return new WaitForSeconds(shotsInterval);
			}
			InUse = false;
			if (Uses <= 0)
			{
				HUDManager.Inst.SpecialHUD.Special = null;
				Destroy(gameObject);
			}
		}
	}
}
