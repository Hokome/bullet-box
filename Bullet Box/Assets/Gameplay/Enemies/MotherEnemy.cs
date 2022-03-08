using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class MotherEnemy : Enemy
	{
		[SerializeField] private float speed;
		[Header("Spawning")]
		[SerializeField] private Egg egg;
		[SerializeField] private float spawnRate;
		[SerializeField] private float radius;
		[SerializeField] private Color particlesColor;

		private Vector2 delta;

		protected override void Start()
		{
			base.Start();
			StartCoroutine(SpawnRoutine());
		}

		private void FixedUpdate()
		{
			Vector2 v = Utility.RandomCircle(1f);
			delta += v * Time.fixedDeltaTime;
			Vector2.ClampMagnitude(delta, 1f);
			if (MathEx.MaxSpeed(rb.velocity, delta))
				rb.velocity += speed * Time.fixedDeltaTime * delta;
		}

		private IEnumerator SpawnRoutine()
		{
			while (true)
			{
				yield return new WaitForSeconds(spawnRate);
				Egg egg = Instantiate(this.egg);
				egg.transform.position = transform.position;
				egg.transform.Rotate(0f, 0f, Random.Range(0f, 360f));
				egg.transform.Translate(radius, 0f, 0f);
			}
		}
	}
}
