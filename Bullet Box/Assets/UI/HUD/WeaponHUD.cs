using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BulletBox.UI
{
    public class WeaponHUD : WeaponUI
    {
		[Space]
		[SerializeField] protected Image background;
		[SerializeField] private Sprite unselectedSprite;
		[SerializeField] private Sprite selectedSprite;
		[SerializeField] private Sprite emptySprite;

		public override Weapon Weapon
		{
			get => base.Weapon;
			set
			{
				base.Weapon = value;
				if (weapon == null)
					Selected = false;
			}
		}

		private bool selected;
		public bool Selected
		{
			get => selected;
			set
			{
				if (weapon == null)
				{
					background.sprite = emptySprite;
					selected = false;
					return;
				}

				selected = value;
				background.sprite = selected ? selectedSprite : unselectedSprite;
			}
		}
	}
}
