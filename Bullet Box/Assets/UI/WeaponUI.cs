using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BulletBox.UI
{
    public class WeaponUI : MonoBehaviour
    {
		[SerializeField] protected Image icon;
		[SerializeField] protected TMP_Text nameText;

		protected Weapon weapon;
		public virtual Weapon Weapon
		{
			get => weapon;
			set
			{
				weapon = value;
				if (weapon == null)
				{
					icon.enabled = false;
					if (nameText != null)
						nameText.enabled = false;
					return;
				}

				icon.enabled = true;

				if (nameText != null)
				{
					nameText.enabled = true;
					nameText.text = weapon.ID;
				}

				icon.sprite = weapon.Icon;
			}
		}

	}
}
