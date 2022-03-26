using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class BulletStorm : SpecialWeapon, IShooter
    {
		[SerializeField] private ProjectilePattern pattern;
		[SerializeField] private int totalShots;
		[SerializeField] private float shotsInterval;
		[SerializeField] private float rotationAngle;

		public bool CanShoot => true;

		public Transform Transform => transform;

		public float AttackSpeed => 1f;

		public MonoBehaviour Behaviour => this;

		public override void Use()
		{
			InUse = true;
			Uses--;
			StartCoroutine(ShotsCoroutine());
		}

		private IEnumerator ShotsCoroutine()
		{
			for (int i = 0; i < totalShots; i++)
			{
				StartCoroutine(pattern.ShootOnce(this));
				yield return new WaitForSeconds(shotsInterval);
				transform.Rotate(0f, 0f, rotationAngle);
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
