using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BulletBox
{
    public class Spawner : MonoBehaviour
    {
		[SerializeField] protected Vector2 spawnRange;
		[SerializeField] private Spawnable[] spawns;
		[SerializeField] private float initialDelay;
		[SerializeField] private float budgetIncrease;
		[SerializeField] private float budgetFrequency;
		[SerializeField] private float budgetPostpone;
		private float budget;

		private void Start()
		{
			StartCoroutine(SpawnRoutine());
			if (budgetFrequency > 0f)
				StartCoroutine(BudgetRoutine());
		}

		private IEnumerator SpawnRoutine()
		{
			yield return new WaitForSeconds(initialDelay);
			while (true)
			{
				budget += budgetIncrease;
				Spawnable s = spawns[Random.Range(0, spawns.Length)];
				if (s.SpawnCost > budget)
				{
					yield return new WaitForSeconds(budgetPostpone);
				}

				Spawn(s);
				budget -= s.SpawnCost;
				yield return new WaitForSeconds(s.SpawnCooldown);
			}
		}
		private IEnumerator BudgetRoutine()
		{
			while (true)
			{
				budget += budgetIncrease;
				yield return new WaitForSeconds(budgetFrequency);
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