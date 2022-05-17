using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BulletBox
{
    public class ChargeCrosshair : Crosshair
    {
		[SerializeField] private Image indicator;

		public float ChargeValue
		{
			set
			{
				indicator.fillAmount = value;
			}
		}
	}
}
