using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BulletBox.UI
{
    public class WeaponSelectionUI : WeaponUI
    {
		[SerializeField] private TMP_Text description;
		[SerializeField] private TMP_Text price;


		private Button button;
		public Button Button
		{
			get
			{
				if (button == null)
					button = GetComponentInChildren<Button>();
				return button;
			}
			private set => button = value;
		}

		public override Weapon Weapon
		{
			get => base.Weapon;
			set
			{
				base.Weapon = value;
				price.text = "-";
				description.text = value.Description;
				Button.interactable = true;
				Button.onClick.RemoveAllListeners();
				Button.onClick.AddListener(() => Button.interactable = false);
				Button.onClick.AddListener(() => GameMenu.Inst.SelectWeapon(weapon));
			}
		}
	}
}
