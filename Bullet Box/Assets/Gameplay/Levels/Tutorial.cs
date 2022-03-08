using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class Tutorial : LevelSpawner
    {
		[SerializeField] private WeaponBox weaponBox;
		[SerializeField] private SpecialWeaponBox specialBox;

		protected override IEnumerator SpawnRoutine()
		{
			UI.TutorialHUD hud = HUDManager.Inst.Tutorial;

			hud.Display(hud.Move, true);
			yield return new WaitUntil(() => Player.Inst.transform.position.sqrMagnitude > 3f);
			hud.Display(hud.Move, false);

			hud.Display(hud.Shoot, true);
			yield return new WaitUntil(() => Player.Inst.HasShot);
			hud.Display(hud.Shoot, false);

			Weapon weaponId = Player.Inst.Weapon;
			hud.Display(hud.Switch, true);
			yield return new WaitUntil(() => Player.Inst.Weapon.ID != weaponId.ID);
			hud.Display(hud.Switch, false);

			hud.Display(hud.Ability, true);
			yield return new WaitUntil(() => Player.Inst.HasUsedAbility);
			hud.Display(hud.Ability, false);

			hud.Display(hud.PickUp, true);
			weaponBox.gameObject.SetActive(true);
			yield return new WaitUntil(() => weaponBox == null);
			specialBox.gameObject.SetActive(true);
			yield return new WaitUntil(() => specialBox == null);
			hud.Display(hud.PickUp, false);

			hud.Display(hud.Special, true);
			yield return new WaitUntil(() => Player.Inst.HasUsedSpecial);
			hud.Display(hud.Special, false);
		}
	}
}
