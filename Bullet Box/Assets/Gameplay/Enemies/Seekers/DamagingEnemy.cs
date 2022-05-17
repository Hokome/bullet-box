using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class DamagingEnemy : Enemy
    {
		[SerializeField] private float damage;

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag(Player.Inst.tag))
			{
				Player.Inst.Hit(damage);
			}
		}
	}
}
