using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Egg : Enemy
    {
		[SerializeField] private float speed;
		[SerializeField] private float spawnDelay;
		[SerializeField] private Spawnable spawn;

		protected override void Start()
		{
			base.Start();
			rb.velocity = transform.right * speed;
			StartCoroutine(Hatch());
		}
		private IEnumerator Hatch()
		{
			yield return new WaitForSeconds(spawnDelay);
			Instantiate(spawn).transform.position = transform.position;
			Kill();
		}
	}
}
