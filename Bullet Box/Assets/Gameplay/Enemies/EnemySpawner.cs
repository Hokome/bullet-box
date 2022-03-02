using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class EnemySpawner : Spawner
    {
		[SerializeField] private ParticleSystem spawnParticles;

		public override void Spawn(Spawnable s)	=> StartCoroutine(SpawnDelay(s));

		private IEnumerator SpawnDelay(Spawnable s)
		{
			Vector3 spawnPoint = RandomPosition();
			spawnPoint.z = 1;

			Instantiate(spawnParticles).transform.position = spawnPoint;
			yield return new WaitForSeconds(spawnParticles.main.duration);
			spawnPoint.z = 0;
			s = Instantiate(s);
			s.transform.position = spawnPoint;
		}
	}
}
