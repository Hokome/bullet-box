using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class GameManager : MonoSingleton<GameManager>
    {
		public static TimeSpan CurrentTime => TimeSpan.FromSeconds(Time.time - startTime);
		public static GameMode GameMode { get; private set; }
		public static IEnemySpawner enemySpawner;
		private static float startTime;

		[SerializeField] private GameMode mode;
		private bool gameEnded;

		private void Start()
		{
			startTime = Time.time;
			GameMode = mode;
		}

		private void Update()
		{
			if (gameEnded) return;
			TimeSpan span = CurrentTime;
			HUDManager.Inst.Timer.text = 
				$"{span.Minutes:00}:{span.Seconds:00}.{span.Milliseconds:000}";
		}

		public void GameOver()
		{
			Time.timeScale = 0f;
			gameEnded = true;
			if (GameMode == GameMode.Freeplay)
			{
				Save.Current.freeplayScores.Add(CurrentTime);
				Save.Current.Write();
			}

			HUDManager.Inst.enabled = false;
			PauseMenu.Inst.enabled = false;
			GameMenu.Inst.GameOver(false);
		}
		public void WinGame()
		{
			Time.timeScale = 0f;
			gameEnded = true;
			Save.Current.arcadeScores.Add(CurrentTime);
			Save.Current.Write();

			HUDManager.Inst.enabled = false;
			PauseMenu.Inst.enabled = false;
			GameMenu.Inst.GameOver(true);
		}
	}
	[System.Serializable]
	public enum GameMode
	{
		Arcade,
		Freeplay
	}
}
