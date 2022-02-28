using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Originally from AssetFactory
namespace AssetFactory
{
	[RequireComponent(typeof(CanvasGroup))]
	public class HUDManager : MonoSingleton<HUDManager>
    {
		#region References
		private CanvasGroup main;
		#endregion

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
