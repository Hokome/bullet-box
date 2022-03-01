using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class ShootingEnemy : Enemy
    {
		[SerializeField] private float acceleration;
		[SerializeField] private float maxSpeed;
		[SerializeField] private float minDist;
		[SerializeField] private float maxDist;
		[Space]
		[SerializeField] private Weapon weapon;

		private float c_sqrMinDist;
		private float c_sqrMaxDist;

		private void Awake()
		{
			c_sqrMinDist = minDist * minDist;
			c_sqrMaxDist = maxDist * maxDist;
		}

		private void Update()
		{
			Vector2 v = Player.Inst.transform.position - transform.position;
			weapon.transform.right = v;
			weapon.Fire();
		}

		private void FixedUpdate()
		{
			Vector2 v = Player.Inst.transform.position - transform.position;
			if (v.sqrMagnitude < c_sqrMinDist)
			{
				Vector2 vn = -v.normalized;
				if (MathEx.MaxSpeed(rb.velocity, vn * maxSpeed))
					rb.velocity += acceleration * Time.fixedDeltaTime * vn;
			}
			else if (v.sqrMagnitude > c_sqrMaxDist)
			{
				Vector2 vn = v.normalized;
				if (MathEx.MaxSpeed(rb.velocity, vn * maxSpeed))
					rb.velocity += acceleration * Time.fixedDeltaTime * vn;
			}
		}
	}
}
