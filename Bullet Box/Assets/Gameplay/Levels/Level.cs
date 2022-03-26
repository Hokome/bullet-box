using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	[Serializable]
    public class Level
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
					ret += s.Count;
				return ret;
			}
		}
	}
	[Serializable]
	public struct SubWave
	{
		[SerializeField] private Spawnable spawn;
		[SerializeField] private Transform anchor;
		[SerializeField] private int count;
		[Header("Timing")]
		[SerializeField] private float delay;
		[SerializeField] private float spacing;
		[Header("Spread")]
		[SerializeField] private float radius;
		[SerializeField] private float minDistance;
		public Spawnable Spawn => spawn;
		public Transform Anchor => anchor;
		public int Count => count;
		public float Delay => delay;
		public float Radius => radius;
		public float Spacing => spacing;
		public float MinDistance => minDistance;
	}
}
