using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox.UI
{
    public class TutorialHUD : MonoBehaviour
    {
		[SerializeField] private CanvasGroup move;
		[SerializeField] private CanvasGroup shoot;
		[SerializeField] private CanvasGroup switchWeapon;
		[SerializeField] private CanvasGroup ability;
		[SerializeField] private CanvasGroup pickUp;
		[SerializeField] private CanvasGroup special;
		public CanvasGroup Move => move;
		public CanvasGroup Shoot => shoot;
		public CanvasGroup Switch => switchWeapon;
		public CanvasGroup Ability => ability;
		public CanvasGroup PickUp => pickUp;
		public CanvasGroup Special => special;

		public void Display(CanvasGroup cg, bool value)
		{
			cg.alpha = value ? 1f : 0f;
		}
	}
}
