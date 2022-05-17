using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class Heal : PowerUp
    {
		[SerializeField] private int amount;

		public override void OnPickUp(Player p)
		{
			p.Health += amount;
		}
	}
}
