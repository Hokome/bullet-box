using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BulletBox
{
    public class LevelSpawner : MonoSingleton<LevelSpawner>, IEnemySpawner
    {
		[SerializeField] protected Level[] levels;

		[SerializeField] private ParticleSystem spawnParticles;
		[SerializeField] private Color particlesColor = Color.white;

		private int levelIndex;
		protected int spawnsLeft;
		protected int enemiesLeft;

		private Level Level => levels[levelIndex];

		public void NotifySpawn() => enemiesLeft++;
		public void NotifyDeath() => enemiesLeft--;

		public void NextLevel()
		{
			Time.timeScale = 1f;
			StartCoroutine(SpawnRoutine());
		}

		public void Spawn(Spawnable s, Vector3 p, Color c) => StartCoroutine(SpawnDelay(s, p, c));


		private void Start()
		{
			GameManager.enemySpawner = this;
			NextLevel();
		}
		protected virtual IEnumerator SpawnRoutine()
		{
			for (int i = 0; i < Level.waves.Length; i++)
			{
				SpawnWave(Level[i]);
				yield return new WaitUntil(WaveCompleted);
			}
			levelIndex++;
			if (levelIndex < levels.Length)
			{
				Time.timeScale = 0f;
				GameMenu.Inst.ClearLevel(levelIndex);
				yield break;
			}
			GameManager.Inst.WinGame();
		}
		protected void SpawnWave(Wave w)
		{
			spawnsLeft = w.TotalCount;
			foreach (SubWave sw in w.SubWaves)
			{
				StartCoroutine(SubWaveRoutine(sw));
			}
		}
		private IEnumerator SubWaveRoutine(SubWave wave)
		{
			yield return new WaitForSeconds(wave.Delay);
			float cachedDistance = wave.MinDistance * wave.MinDistance;
			List<Vector2> randoms = new List<Vector2>(wave.Count);
			for (int i = 0; i < wave.Count; i++)
			{
				Vector2 random;
				do
				{
					random = Utility.RandomCircle(wave.Radius);
				}
				while (randoms.Any(v => (v - random).sqrMagnitude < cachedDistance));
				randoms.Add(random);

				StartCoroutine(SpawnDelay(wave.Spawn, wave.Anchor.position + (Vector3)random, particlesColor));
				if (wave.Spacing > 0f)
					yield return new WaitForSeconds(wave.Spacing);
			}
		}
		private IEnumerator SpawnDelay(Spawnable s, Vector3 pos, Color c)
		{
			pos.z++;
			ParticleSystem ps = Instantiate(spawnParticles);
			ps.transform.position = pos;
			var trails = ps.trails;
			trails.colorOverTrail = new ParticleSystem.MinMaxGradient(c);

			pos.z--;
			yield return new WaitForSeconds(spawnParticles.main.duration);
			s = Instantiate(s);
			s.transform.position = pos;
			spawnsLeft--;
		}
		protected bool WaveCompleted() => spawnsLeft <= 0 && enemiesLeft <= 0;

//#if UNITY_EDITOR
//		[SerializeField] private int editorGizmosLevel;
//		[SerializeField] private int editorGizmosWave;
//		[SerializeField] private Color editorColor = Color.cyan;
//		private void OnDrawGizmos()
//		{
//			if (editorGizmosLevel < 0 || editorGizmosWave < 0) return;
//			if (levels == null || levels.Length <= editorGizmosLevel) return;
//			if (Level.waves == null || Level.waves.Length <= editorGizmosWave) return;

//			foreach (SubWave sw in levels[editorGizmosLevel][editorGizmosWave].SubWaves)
//			{
//				foreach (Vector2 pos in sw.Positions)
//				{
//					DebugEx.DrawCross(pos, editorColor, 0.4f);
//				}
//			}
//		}
//#endif
	}
}
