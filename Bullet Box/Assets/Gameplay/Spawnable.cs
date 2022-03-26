using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public abstract class Spawnable : MonoBehaviour
    {
		[SerializeField] private float spawnCooldown = 1;
		[SerializeField] private float spawnCost;
		[SerializeField] private float spawnChance;
		public float SpawnCooldown => spawnCooldown;
		public float SpawnCost => spawnCost;
		public float SpawnChance => spawnChance;
	}
}
