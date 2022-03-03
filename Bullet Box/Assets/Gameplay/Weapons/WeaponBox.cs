using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BulletBox
{
    public class WeaponBox : Pickupable
    {
		[SerializeField] private Weapon weapon;
		[SerializeField] private TMP_Text text;

		public Weapon Weapon => weapon;

		private void Start()
		{
			text.text = weapon.name;
		}

		public override void PickUp(Player p)
		{
			p.Weapon = weapon;
			Destroy(gameObject);
		}
	}
}
