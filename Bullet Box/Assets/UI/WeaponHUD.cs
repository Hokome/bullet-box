using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BulletBox.UI
{
    public class WeaponHUD : MonoBehaviour
    {
		[SerializeField] private Image background;
		[SerializeField] private TMP_Text text;
		[Space]
		[SerializeField] private Sprite unselectedSprite;
		[SerializeField] private Sprite selectedSprite;
		[SerializeField] private Sprite emptySprite;

		private Weapon weapon;
		private bool selected;
		public Weapon Weapon
		{
			get => weapon;
			set
			{
				weapon = value;
				if (weapon == null)
				{
					Selected = false;
					text.text = string.Empty;
				}
				text.text = weapon.name[0].ToString();
			}
		}

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
