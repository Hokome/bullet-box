using BulletBox.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Originally from AssetFactory
namespace BulletBox
{
    public class MainMenu : MenuManager
    {
		public static MainMenu Inst { get; private set; }

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
				Debug.LogWarning($"Singleton for {typeof(MainMenu)} already exists.");
				Destroy(gameObject);
			}
		}

		public void StartGame(int sceneIndex)
		{
			SceneTransitioner.Inst.LoadScene(sceneIndex, delegate
			{
				HUDManager.Inst.enabled = true;
				PauseMenu.Inst.enabled = true;
				//GameMenu.Inst.enabled = true;
				enabled = false;
			});
		}
		public void LeaveGame()
		{
			PauseMenu.Inst.Paused = false;
			SceneTransitioner.Inst.LoadScene(1, delegate
			{
				HUDManager.Inst.enabled = false;
				PauseMenu.Inst.enabled = false;
				//GameMenu.Inst.enabled = false;
				enabled = true;
			});
		}
		public void QuitGame() => Application.Quit();

		protected override void OnEnable()
		{
			base.OnEnable();
			ToMain();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			currentMenu.Display(false);
		}
	}
}
