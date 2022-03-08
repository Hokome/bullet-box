using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BulletBox.UI
{
    public class BossHUD : MonoBehaviour
    {
		[SerializeField] private DoubleGauge healthBar;
		[SerializeField] private TMP_Text bossName;

		private CanvasGroup cGroup;

		public DoubleGauge HealthBar => healthBar;
		public CanvasGroup Group => cGroup;
		private void Start()
		{
			cGroup = GetComponent<CanvasGroup>();
		}

		public void StartBossBattle(Boss boss)
		{
			bossName.text = boss.DisplayName;
			HealthBar.Max = boss.MaxHealth;
			HealthBar.Value = boss.Health;
			HealthBar.ForceUpdate();
			Group.LeanAlpha(1f, 0.5f);
		}
		public void EndBossBattle()
		{
			Group.LeanAlpha(0f, 2f);
		}
	}
}
