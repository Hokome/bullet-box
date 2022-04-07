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
		[Header("Player")]
		[SerializeField] private DoubleGauge healthBar;
		[SerializeField] private Image abilityCooldown;
		[SerializeField] private WeaponHUD[] weapons;
		[SerializeField] private SpecialHUD specialHUD;
		[Space]
		[SerializeField] private TutorialHUD tutorial;
		[SerializeField] private TMP_Text levelClearedText;
		[SerializeField] private CanvasGroup levelClearedGroup;
		[SerializeField] private TMP_Text timer;
		[Space]
		[SerializeField] private BossHUD bossHUD;
		[SerializeField] private DoubleGauge experience;

		private CanvasGroup main;

		public DoubleGauge HealthBar => healthBar;
		public DoubleGauge Experience => experience;
		public WeaponHUD[] WeaponHUDs => weapons;
		public SpecialHUD SpecialHUD => specialHUD;
		public TutorialHUD Tutorial => tutorial;
		public TMP_Text Timer => timer;
		public BossHUD BossHUD => bossHUD;
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
			BossHUD.Group.alpha = 0f;
		}
		private void OnDisable()
		{
			EnableMain(false);
		}
	}
}
