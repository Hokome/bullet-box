using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class Spawner : MonoBehaviour
    {
		[SerializeField] private Vector2 spawnRange;
		[SerializeField] private Enemy[] enemies;

		private void Start()
		{
			StartCoroutine(SpawnRoutine());
		}

		private IEnumerator SpawnRoutine()
		{
			while (true)
			{
				Enemy e = enemies[Random.Range(0, enemies.Length)];
				e = Instantiate(e);
				Vector2 spawnPoint = new Vector2(
					Random.Range(-spawnRange.x, spawnRange.x),
					Random.Range(-spawnRange.y, spawnRange.y));
				e.transform.position = spawnPoint;
				yield return new WaitForSeconds(e.SpawnCooldown);
			}
		}
		private void OnDrawGizmosSelected()
		{
			DebugEx.DrawRect(new Rect(-spawnRange, spawnRange * 2), Color.blue);
		}
	}
}
