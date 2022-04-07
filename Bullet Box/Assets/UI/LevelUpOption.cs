using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BulletBox.UI
{
    public class LevelUpOption : ButtonEvent<int>
    {
		[SerializeField] private Image icon;
		[SerializeField] private TMP_Text level;
		[SerializeField] private TMP_Text weaponName;
		[SerializeField] private TMP_Text description;

		public Button Button { get; private set; }

		private Weapon weapon;
		public Weapon Weapon
		{
			get => weapon;
			set
			{
				weapon = value;
				icon.sprite = weapon.Icon;
				if (weapon.Level > 0)
					level.text = $"LVL {weapon.Level}";
				else
					level.text = "NEW";
				weaponName.text = weapon.ID;
				description.text = weapon.GetNextLevelDescription();
			}
		}

		private void Start()
		{
			Button = GetComponent<Button>();
		}

		public void SetLevelMax(Weapon weapon)
		{
			this.weapon = weapon;
			icon.sprite = weapon.Icon;
			level.text = $"LVL MAX";
			description.text = "Cannot upgrade this further.";
		}
	}
}
