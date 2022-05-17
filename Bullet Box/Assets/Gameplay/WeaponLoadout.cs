using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public struct WeaponLoadout
    {
		public WeaponLoadout(int weaponCount = 2)
		{
			weapons = new Weapon[weaponCount];
			ability = null;
		}

		public Weapon[] weapons;
		public Ability ability;
    }
}
