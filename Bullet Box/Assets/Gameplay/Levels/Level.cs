using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{

	[CreateAssetMenu(fileName = "Level 0", menuName = "Level", order = 1)]
    public class Level : ScriptableObject
    {
		public Wave[] waves;
		public Wave this[int index] => waves[index];
	}
	[Serializable]
	public class Wave
	{
		[SerializeField] private SubWave[] subWaves;
		public SubWave[] SubWaves => subWaves;
		public int TotalCount
		{
			get
			{
				int ret = 0;
				foreach (var s in subWaves)
					ret += s.Spawns.Length;
				return ret;
			}
		}
	}
	[Serializable]
	public struct SubWave
	{
		[SerializeField] private SpawnData[] spawns;
		[SerializeField] private float delay;
		[SerializeField] private float spacing;
		public float Spacing => spacing;
		public float Delay => delay;
		public SpawnData[] Spawns => spawns;
	}
	[Serializable]
	public struct SpawnData
	{
		[SerializeField] private Vector2 position;
		[SerializeField] private Spawnable spawn;
		public Vector2 Position => position;
		public Spawnable Spawn => spawn;
	}
}
