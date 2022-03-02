using BulletBox.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
		[SerializeField] private Image abilityCooldown;
		[SerializeField] private WeaponHUD[] weapons;
		[SerializeField] private TMP_Text timer;

		private CanvasGroup main;

		public DoubleGauge HealthBar => healthBar;
		public WeaponHUD[] WeaponHUDs => weapons;
		public TMP_Text Timer => timer;
		#endregion

		#region Part enabling
		public void EnableMain(bool value)
		{
			main.alpha = value ? 1f : 0f;
		}
		#endregion

		public void SetAbilityCooldown(float cooldown)
		{
			LeanTween.value(
				abilityCooldown.gameObject,
				f => abilityCooldown.fillAmount = f, 0f, 1f, cooldown);
		}

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
