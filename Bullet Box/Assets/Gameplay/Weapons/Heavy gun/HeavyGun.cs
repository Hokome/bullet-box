using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class HeavyGun : Weapon
	{
		[SerializeField] private float maxCharge;
		[SerializeField] private float chargeBurn;
		[SerializeField] private AnimationCurve chargeCurve;


		private float ChargeRatio => Charge / maxCharge;
		private float TimeRatio => chargeTime / maxCharge;

		private float chargeTime;

		private float charge;
		public float Charge
		{
			get => charge;
			set
			{
				charge = Mathf.Clamp(value, 0f, maxCharge);
				Crosshair.ChargeValue = ChargeRatio;
			}
		}

		private bool charging;

		protected new ChargeCrosshair Crosshair => base.Crosshair as ChargeCrosshair;

		protected override void Update()
		{
			if (isFiring)
			{
				Charge -= Time.deltaTime * chargeBurn;
				if (Charge > 0f)
					TryShoot();
				else
					isFiring = false;
			}
			else if (charging && Charge < maxCharge)
			{
				Charge = chargeCurve.Evaluate(TimeRatio) * maxCharge;
				chargeTime += Time.deltaTime;
			}
		}

		protected override void OnDisable()
		{
			charging = false;
			Charge = 0f;
			base.OnDisable();
		}
		public override void OnFireDown()
		{
			charging = true;
			chargeTime = 0f;
		}
		public override void OnFireUp()
		{
			if (charging)
			{
				isFiring = true;
				charging = false;
			}
		}
	}
}
