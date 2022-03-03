using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class WeaponSpawner : Spawner
    {
		protected override Spawnable Select()
		{
			WeaponBox b;
			do
			{
				b = (WeaponBox)base.Select();
			} while (Player.Inst.HasWeapon(b.Weapon));
			return b;
		}
	}
}
