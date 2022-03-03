using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BulletBox
{
    public class SpecialHUD : MonoBehaviour
    {
		[SerializeField] private TMP_Text nameText;
		[SerializeField] private TMP_Text usesText;

		private SpecialWeapon special;
		private int maxUses;

		public SpecialWeapon Special
		{
			get => special;
			set
			{
				gameObject.SetActive(value != null);
				special = value;
				if (special == null)
					return;
				//Remove "(clone)" before displaying name
				string name = special.name.Substring(0, special.name.Length - 7);
				nameText.text = name;
				maxUses = special.Uses;
				UpdateUses();
			}
		}

		public void UpdateUses()
		{
			usesText.text = $"{special.Uses}/{maxUses}";
		}
	}
}
