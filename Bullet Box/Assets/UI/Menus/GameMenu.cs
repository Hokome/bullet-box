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
		[SerializeField] private Menu weaponSelectionMenu;
		[SerializeField] private Transform weaponParent;
		[SerializeField] private WeaponSelectionUI weaponSelectionPrefab;
		[SerializeField] private Button goButton;
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

		private WeaponLoadout loadout;

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
	
		private LTDescr gameOver;
		public void GameOver(bool won)
		{
			timerText.text = HUDManager.Inst.Timer.text;
			gameOverText.text = won ? "Finished!" : "Game Over";
			gameOverGroup.blocksRaycasts = true;
			gameOverGroup.interactable = false;
			Cursor.visible = true;

			gameOver = LeanTween.alphaCanvas(gameOverGroup, 1f, 2f); 
			gameOver.setOnComplete(() =>
			{
				gameOverGroup.interactable = true;
				gameOver = LeanTween.alphaCanvas(menuButton.GetComponent<CanvasGroup>(), 1f, 1f)
				.setIgnoreTimeScale(true);
			});
			gameOver.setIgnoreTimeScale(true);
		}
		public void SelectScreen()
		{
			Time.timeScale = 0f;
			SoleSelect(weaponSelectionMenu);
			HUDManager.Inst.EnableMain(false);
			Cursor.visible = true;
			goButton.interactable = false;
			loadout = new WeaponLoadout(GameManager.Rules.MaxWeapons);
			weaponParent.DestroyChildren();
			foreach (Weapon w in GameManager.Rules.availableWeapons)
			{
				WeaponSelectionUI ui = Instantiate(weaponSelectionPrefab, weaponParent);
				ui.Weapon = w;
			}
		}
		public void SelectWeapon(Weapon w)
		{
			for (int i = 0; i < loadout.weapons.Length; i++)
			{
				if (loadout.weapons[i] == null)
				{
					loadout.weapons[i] = w;
					break;
				}
				else if (loadout.weapons[i] == w) return;
			}
			if (loadout.weapons.All(w => w != null))
				goButton.interactable = true;

		}
		public void StartGame()
		{
			if (loadout.weapons.Any(w => w == null)) return;
			CurrentMenu.Display(false);
			HUDManager.Inst.EnableMain(true);
			GameManager.Inst.StartGame(loadout);
			Cursor.visible = false;
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
