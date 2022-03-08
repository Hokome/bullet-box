using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public abstract class SpecialWeapon : MonoBehaviour
    {
		[SerializeField] private int uses = 1;
		public int Uses
		{
			get => uses;
			protected set
			{
				uses = value;
				HUDManager.Inst.SpecialHUD.UpdateUses();
			}
		}
		public bool InUse { get; protected set; }

		public abstract void Use();
	}
}
