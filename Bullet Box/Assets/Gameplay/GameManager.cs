using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class GameManager : MonoSingleton<GameManager>
    {
		public static TimeSpan CurrentTime => TimeSpan.FromSeconds(Time.time - startTime);
		public static GameMode GameMode => Inst.mode;
		public static float Difficulty { get; set; } = 1f;
		public static IEnemySpawner enemySpawner;
		private static float startTime;

		[SerializeField] private GameMode mode;
		[SerializeField] private GameRules rules;
		[SerializeField] private Player playerPrefab;
		[SerializeField] private SpawnZone[] playerSpawns;
		[SerializeField] private Cinemachine.CinemachineVirtualCamera playerCamera;

		public static GameRules Rules => Inst.rules;
		private bool gameEnded;

		public int MaxLevel { get; set; }

		private void Start()
		{
			GameMenu.Inst.SelectScreen();
		}

		public void StartGame(WeaponLoadout loadout)
		{
			Time.timeScale = 1f;
			startTime = Time.time;

			Player p = Instantiate(playerPrefab);
			p.transform.position = playerSpawns.GetRandom().GetPosition();
			p.SetLoadout(loadout);

			playerCamera = Instantiate(playerCamera);
			playerCamera.transform.position = p.transform.position;
			playerCamera.Follow = p.transform;
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
	public enum GameMode
	{
		Arcade = 3,
		Freeplay = 4
	}
}
