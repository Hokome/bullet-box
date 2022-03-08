using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class EnemySpawner : Spawner, IEnemySpawner
    {
		public static EnemySpawner Inst { get; private set; }

		[SerializeField] private ParticleSystem spawnParticles;
		[SerializeField] private Color particlesColor = Color.white;

		private void Awake()
		{
			GameManager.enemySpawner = this;
		}

		protected override Spawnable Select()
		{
			Spawnable s = base.Select();
			DebugEx.SetElement(0, budget);
			DebugEx.SetElement(1, $"s: {s.name}, c:{s.SpawnCost}");
			return s;
		}

		public override void Spawn(Spawnable s, Vector3 pos) => Spawn(s, pos, particlesColor);
		public void Spawn(Spawnable s, Vector3 pos, Color c) => StartCoroutine(SpawnDelay(s, pos, c));

		private IEnumerator SpawnDelay(Spawnable s, Vector3 pos, Color c)
		{
			pos.z = 1;
			ParticleSystem ps = Instantiate(spawnParticles);
			ps.transform.position = pos;
			var trails = ps.trails;
			trails.colorOverTrail = new ParticleSystem.MinMaxGradient(c);

			pos.z = 0;
			yield return new WaitForSeconds(spawnParticles.main.duration);
			s = Instantiate(s);
			s.transform.position = pos;
		}
	}
}
