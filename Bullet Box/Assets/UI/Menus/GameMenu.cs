using BulletBox.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Originally from AssetFactory
namespace BulletBox
{
    public class GameMenu : MenuManager
    {
		public static GameMenu Inst { get; private set; }
		[SerializeField] private CanvasGroup gameOverGroup;
		[SerializeField] private TMP_Text gameOverText;
		[SerializeField] private Button menuButton;
		[Space]
		[SerializeField] private TMP_Text timerText;
		[Space]
		[SerializeField] private Menu levelClearMenu;
		[SerializeField] private TMP_Text levelClearText;
		[Space]
		[SerializeField] private Menu levelUpMenu;
		[SerializeField] private Menu weaponSwitchMenu;
		[SerializeField] private LevelUpOption[] levelUpOptions;
		[SerializeField] private TMP_Text[] weaponSlots;

		private Weapon weaponChoice;

		//Can be useful if the game has pop up menus only.

		//public override void Back()
		//{
		//	if (!backEnabled) return;
		//	CurrentMenu.Display(false);
		//	if (navigationStack.Count > 0)
		//	{
		//		CurrentMenu = navigationStack.Pop();
		//		CallOnBack();
		//	}
		//}

		private void Awake()
		{
			if (Inst == null)
			{
				Inst = this;
				DontDestroyOnLoad(gameObject);
				currentMenu = main;
			}
			else
			{
				Debug.LogWarning($"Singleton for {typeof(PauseMenu)} already exists.");
				Destroy(gameObject);
			}
		}
		public void ClearLevel(int level)
		{
			levelClearText.text = $"Level {level}\nCleared!";
			SoleSelect(levelClearMenu);
		}
		public void NextLevel()
		{
			levelClearMenu.Display(false);
			LevelSpawner.Inst.NextLevel();
		}
		public void LevelUp()
		{
			HUDManager.Inst.EnableMain(false);
			SoleSelect(levelUpMenu);
			int i = 0;
			while (i < Player.Inst.Weapons.Length)
			{
				if (Player.Inst.Weapons[i].IsMaxLevel)
				{
					levelUpOptions[i].Button.interactable = false;
					levelUpOptions[i].SetLevelMax(Player.Inst.Weapons[i]);
				}
				else
				{
					levelUpOptions[i].Button.interactable = true;
					levelUpOptions[i].Weapon = Player.Inst.Weapons[i];
					levelUpOptions[i].identifier = i;
				}
				i++;
			}
			while (i < levelUpOptions.Length)
			{
				Weapon weapon = GameManager.Inst.weaponsList.SelectRandom(w => !Player.Inst.HasWeapon(w));
				levelUpOptions[i].identifier = i;
				levelUpOptions[i].Weapon = weapon;
				i++;
			}
		}
		public void ApplyLevelUp(int index)
		{
			if (index < Player.Inst.Weapons.Length)
			{
				Player.Inst.Weapons[index].Level++;
				Resume();
			}
			else
			{
				weaponChoice = levelUpOptions[index].Weapon;
				for (int i = 0; i < weaponSlots.Length; i++)
				{
					weaponSlots[i].text = Player.Inst.Weapons[i].ID;
				}
				SoleSelect(weaponSwitchMenu);
			}
		}
		public void SwitchWeapon(int index)
		{
			Player.Inst.SetWeapon(index, weaponChoice);
			Resume();
		}
		private void Resume()
		{
			HUDManager.Inst.EnableMain(true);
			CurrentMenu.Display(false);
			Time.timeScale = 1f;
		}

		private LTDescr gameOver;
		public void GameOver(bool won)
		{
			timerText.text = HUDManager.Inst.Timer.text;
			gameOverText.text = won ? "Finished!" : "Game Over";
			gameOverGroup.blocksRaycasts = true;
			gameOverGroup.interactable = false;

			gameOver = LeanTween.alphaCanvas(gameOverGroup, 1f, 2f); 
			gameOver.setOnComplete(() =>
			{
				gameOverGroup.interactable = true;
				gameOver = LeanTween.alphaCanvas(menuButton.GetComponent<CanvasGroup>(), 1f, 1f)
				.setIgnoreTimeScale(true);
			});
			gameOver.setIgnoreTimeScale(true);
		}
		protected override void OnEnable()
		{
			base.OnEnable();
			Utility.ShowCanvas(gameOverGroup, false);
		}
		protected override void OnDisable()
		{
			base.OnDisable();
			if (gameOver != null)
					LeanTween.cancel(gameOver.uniqueId);
			Utility.ShowCanvas(gameOverGroup, false);
		}
	}
}
