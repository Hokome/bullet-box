using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class ExperiencePoint : MonoBehaviour
    {
		[SerializeField] private float minSpeed, maxSpeed;
		[SerializeField] private LayerMask mask;

		private float speed;

		private void Awake()
		{
			speed = Random.Range(minSpeed, maxSpeed);
		}

		private void Update()
		{
			Vector2 pos = Player.Inst.transform.position;
			transform.position = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime);
			if (Physics2D.OverlapPoint(transform.position, mask))
			{
				Player.Inst.Experience++;
				Destroy(gameObject);
			}
		}
	}
}
