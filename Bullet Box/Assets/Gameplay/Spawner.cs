using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BulletBox
{
    public class Spawner : MonoBehaviour
    {
		[SerializeField] protected Vector2 spawnRange;
		[SerializeField] protected Spawnable[] spawns;
		[SerializeField] private float initialDelay;
		[SerializeField] private float budgetIncrease;
		[SerializeField] private float budgetFrequency;
		[SerializeField] private float budgetBurst;
		[SerializeField] private float budgetBurstCooldown = Mathf.Infinity;
		protected float budget;
		protected float[] chances;

		private void Start()
		{
			StartCoroutine(SpawnRoutine());
			if (budgetFrequency > 0f)
				StartCoroutine(BudgetRoutine());
		}

		protected virtual Spawnable Select()
		{
			float selection = Random.Range(0f, chances.Sum());
			float current = 0f;
			for (int i = 0; i < chances.Length; i++)
			{
				current += chances[i];
				if (current >= selection)
					return spawns[i];
			}
			return null;
		}
		private IEnumerator SpawnRoutine()
		{
			chances = new float[spawns.Length];
			for (int i = 0; i < chances.Length; i++)
			{
				chances[i] = spawns[i].SpawnChance;
			}
			yield return new WaitForSeconds(initialDelay);
			while (true)
			{
				Spawnable s = Select();
				if (s.SpawnCost > budget)
				{
					yield return null;
					continue;
				}

				Spawn(s);
				budget -= s.SpawnCost;
				yield return new WaitForSeconds(s.SpawnCooldown);
			}
		}
		private float lastBurst;
		private IEnumerator BudgetRoutine()
		{
			while (true)
			{
				if (Time.time - lastBurst >= budgetBurstCooldown)
				{
					budget += budgetBurst;
					lastBurst = Time.time;
				}
				else
				{
					budget += budgetIncrease;
				}
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
