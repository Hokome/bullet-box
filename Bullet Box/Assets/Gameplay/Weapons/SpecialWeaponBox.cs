using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class SpecialWeaponBox : Pickupable
    {
		[SerializeField] private SpecialWeapon special;

		public override void PickUp(Player p)
		{
			p.Special = special;
			Destroy(gameObject);
		}
	}
}
