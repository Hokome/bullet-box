using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class Boss : Enemy
    {
		[SerializeField] private string displayName = "Boss";

		public float MaxHealth => maxHealth;
		public string DisplayName => displayName;

		public bool CanShoot => true;
		public float AttackSpeed => 1f;

		protected override void Start()
		{
			base.Start();
			HUDManager.Inst.BossHUD.StartBossBattle(this);
		}

		public override void Hit(float damage)
		{
			base.Hit(damage);
			HUDManager.Inst.BossHUD.HealthBar.Value = Health;
		}

		public override void Kill()
		{
			base.Kill();
			HUDManager.Inst.BossHUD.EndBossBattle();
		}
	}
}
