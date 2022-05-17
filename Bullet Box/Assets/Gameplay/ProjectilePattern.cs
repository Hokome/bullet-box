using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	[System.Serializable]
    public class ProjectilePattern
    {
		public Projectile projectile;
		public float fireRate = 1f;
		public int projectileCount = 1;
		public float totalAngle = 0f;
		public float imprecision = 0f;
		public float spread = 0f;
		public Vector2 offset = new Vector2(0.5f, 0f);
		public bool globalRotation = false;

		public IEnumerator StartPattern(IShooter shooter)
		{
			while (true)
			{
				yield return new WaitForSeconds(shooter.AttackSpeed / fireRate);
				shooter.Behaviour.StartCoroutine(ShootOnce(shooter));
			}
		}
		public IEnumerator ShootOnce(IShooter shooter)
		{
			float increment = totalAngle / projectileCount;
			float startAngle = -totalAngle / 2f;
			float spacing = 0f;
			if (spread > 0f)
				 spacing = spread / projectileCount;
			for (int i = 0; i < projectileCount; i++)
			{
				if (!shooter.CanShoot)
					yield return new WaitUntil(() => shooter.CanShoot);
				Projectile p = Object.Instantiate(projectile, shooter.Transform.position, globalRotation ? Quaternion.identity : shooter.Transform.rotation);
				p.transform.Rotate(0f, 0f, startAngle + (i * increment) + Random.Range(-imprecision, imprecision));
				p.transform.Translate(offset);
				p.gameObject.SetActive(true);
				if (spacing > 0f)
					yield return new WaitForSeconds(spacing / shooter.AttackSpeed);
			}
		}
	}
	/// <summary>
	/// Enables <see cref="ProjectilePattern"/> to get context about the object that is shooting the projectiles.
	/// </summary>
	public interface IShooter
	{
		bool CanShoot { get; }
		Transform Transform { get; }
		float AttackSpeed { get; }
		MonoBehaviour Behaviour { get; }
	}
}
