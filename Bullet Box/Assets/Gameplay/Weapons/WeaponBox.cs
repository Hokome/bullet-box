using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class WeaponBox : Pickupable
    {
		[SerializeField] private Weapon weapon;

		public override void PickUp(Player p)
		{
			p.Weapon = weapon;
			Destroy(gameObject);
		}
	}
}
