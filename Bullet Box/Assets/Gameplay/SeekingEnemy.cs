using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class SeekingEnemy : Enemy
    {
		[SerializeField] private float speed;
		[SerializeField] private float damage;

		private void FixedUpdate()
		{
			Vector2 pPos = Player.Inst.transform.position;
			transform.right = pPos - (Vector2)transform.position;
			rb.velocity = transform.right * speed;
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag(Player.Inst.tag))
			{
				Player.Inst.Hit(damage);
			}
		}
	}
}
