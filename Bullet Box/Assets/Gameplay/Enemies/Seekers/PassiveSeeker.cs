using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox.AI
{
	public class PassiveSeeker : LineOfSightSeeker
    {
		[SerializeField] private LayerMask sightMask;

		private bool detected;

		protected override void FixedUpdate()
		{
			if (!detected)
			{
				Vector2 direction = Player.Inst.Rb.position - rb.position;
				if (!Physics2D.CircleCast(rb.position, col.radius, direction, direction.magnitude, sightMask))
					detected = true;
				else
					return;
			}
			base.FixedUpdate();
		}
	}
}
