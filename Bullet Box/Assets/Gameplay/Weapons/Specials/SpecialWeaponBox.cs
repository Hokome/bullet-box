using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BulletBox
{
    public class SpecialWeaponBox : Pickupable
    {
		[SerializeField] private SpecialWeapon special;
		[SerializeField] private TMP_Text text;

		private void Start()
		{
			text.text = special.name;
		}

		public override void PickUp(Player p)
		{
			p.Special = special;
			Destroy(gameObject);
		}
	}
}
