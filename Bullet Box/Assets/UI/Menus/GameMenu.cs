using BulletBox.UI;
using System.Collections;
using System.Collections.Generic;
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
		[SerializeField] private TMP_Text timerText;
		[SerializeField] private Button menuButton;

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

		private LTDescr gameOver;
		public void GameOver()
		{
			timerText.text = HUDManager.Inst.Timer.text;
			gameOverGroup.blocksRaycasts = true;
			gameOverGroup.interactable = false;

			gameOver = LeanTween.alphaCanvas(gameOverGroup, 1f, 2f); 
			gameOver.setOnComplete(() =>
			{
				gameOverGroup.interactable = true;
				gameOver = LeanTween.alphaCanvas(menuButton.GetComponent<CanvasGroup>(), 1f, 1f)
				.setDelay(2f)
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
