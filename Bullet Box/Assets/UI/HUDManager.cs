using BulletBox.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Originally from AssetFactory
namespace BulletBox
{
	[RequireComponent(typeof(CanvasGroup))]
	public class HUDManager : MonoSingleton<HUDManager>
    {
		#region References
		[SerializeField] private DoubleGauge healthBar;

		private CanvasGroup main;
		#endregion

		public DoubleGauge HealthBar => healthBar;

		#region Part enabling
		public void EnableMain(bool value)
		{
			main.alpha = value ? 1f : 0f;
		}
		#endregion
		protected override void Awake()
		{
			base.Awake();
			DontDestroyOnLoad(gameObject);
			main = GetComponent<CanvasGroup>();
			enabled = false;
		}

		private void OnEnable()
		{
			EnableMain(true);
		}
		private void OnDisable()
		{
			EnableMain(false);
		}
	}
}
