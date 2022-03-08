using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	[CreateAssetMenu(fileName = "New Projectile Pattern", menuName = "Projectile Pattern", order = 1)]
    public class ProjectilePattern : ScriptableObject
    {
		[SerializeField] private Projectile projectile;
		[SerializeField] private float fireRate = 1f;
		[SerializeField] private int projectileCount = 1;
		[SerializeField] private float totalAngle = 0f;
		[SerializeField] private float imprecision = 0f;
		[SerializeField] private float spacing = 0f;
		[SerializeField] private Vector2 offset = Vector2.right * 0.5f;
		[SerializeField] private bool globalRotation = false;

		public Projectile Projectile => projectile;
		public float FireRate => fireRate;
		public int ProjectileCount => projectileCount;
		public float TotalAngle => totalAngle;
		public float Imprecision => imprecision;
		public float Spacing => spacing;
		public Vector2 Offset => offset;
		public bool GlobalRotation => globalRotation;

		public IEnumerator StartPattern(IShooter shooter)
		{
			while (true)
			{
				yield return new WaitForSeconds(shooter.AttackSpeed / FireRate);
				shooter.Behaviour.StartCoroutine(ShootOnce(shooter));
			}
		}
		public IEnumerator ShootOnce(IShooter shooter)
		{
			float increment = TotalAngle / ProjectileCount;
			for (int i = 0; i < ProjectileCount; i++)
			{
				if (!shooter.CanShoot)
					yield return new WaitUntil(() => shooter.CanShoot);
				Projectile p = Instantiate(Projectile);
				p.transform.position = shooter.Transform.position;
				if (!GlobalRotation)
					p.transform.rotation = shooter.Transform.rotation;
				p.transform.Rotate(0f, 0f, i * increment + Random.Range(-Imprecision, Imprecision));
				p.transform.Translate(offset);
				if (Spacing > 0f)
					yield return new WaitForSeconds(Spacing / shooter.AttackSpeed);
			}
		}
	}
	public interface IShooter
	{
		bool CanShoot { get; }
		Transform Transform { get; }
		float AttackSpeed { get; }
		MonoBehaviour Behaviour { get; }
	}
}
