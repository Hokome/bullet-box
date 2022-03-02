using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class Spawner : MonoBehaviour
    {
		[SerializeField] protected Vector2 spawnRange;
		[SerializeField] private Spawnable[] enemies;
		[SerializeField] private float initialDelay;

		private void Start()
		{
			StartCoroutine(SpawnRoutine());
		}

		private IEnumerator SpawnRoutine()
		{
			yield return new WaitForSeconds(initialDelay);
			while (true)
			{
				Spawnable s = enemies[Random.Range(0, enemies.Length)];
				Spawn(s);
				yield return new WaitForSeconds(s.SpawnCooldown);
			}
		}

		public virtual void Spawn(Spawnable s) => Spawn(s, RandomPosition());
		public virtual void Spawn(Spawnable s, Vector3 position)
		{
			Instantiate(s).transform.position = (Vector2)position;
		}
		protected Vector2 RandomPosition() 
			=> new Vector2(
				Random.Range(-spawnRange.x, spawnRange.x),
				Random.Range(-spawnRange.y, spawnRange.y));

		private void OnDrawGizmosSelected()
		{
			DebugEx.DrawRect(new Rect(-spawnRange, spawnRange * 2), Color.blue);
		}
	}
}
