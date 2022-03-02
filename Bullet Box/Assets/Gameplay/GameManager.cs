using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class GameManager : MonoSingleton<GameManager>
    {
		public static TimeSpan CurrentTime => TimeSpan.FromSeconds(Time.time - startTime);
		private static float startTime;

		private bool gameEnded;

		private void Start()
		{
			startTime = Time.time;
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
			Save.Current.scores.Add(CurrentTime);
			Save.Current.Write();

			HUDManager.Inst.enabled = false;
			PauseMenu.Inst.enabled = false;
			GameMenu.Inst.GameOver();
		}
	}
}
