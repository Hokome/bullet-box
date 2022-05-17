using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox.AI
{
	[RequireComponent(typeof(CircleCollider2D))]
	public class LineOfSightSeeker : TargetSeeker
	{
		[SerializeField] private float minDist;
		[SerializeField] private float maxDist;
		[SerializeField] private LayerMask lineOfSightObstacle;

		protected CircleCollider2D col;

		private float sqrMinDist;
		private float sqrMaxDist;

		protected override void Start()
		{
			base.Start();

			col = GetComponent<CircleCollider2D>();

			sqrMinDist = minDist * minDist;
			sqrMaxDist = maxDist * maxDist;
		}

		protected virtual void FixedUpdate()
		{
			if (PathInvalid()) return;

			Vector2 targetLine = destination.target.position - transform.position;
			Vector2 pos = transform.position;
			pos += col.offset;
			RaycastHit2D hit = Physics2D.CircleCast(pos, col.radius, targetLine, targetLine.magnitude, lineOfSightObstacle);
			Vector2 direction;

			if (hit)
				direction = GetDirection().normalized;
			else if (targetLine.sqrMagnitude < sqrMinDist)
				direction = -targetLine.normalized;
			else if (targetLine.sqrMagnitude > sqrMaxDist)
				direction = targetLine.normalized;
			else return;

			if (MathEx.MaxSpeed(rb.velocity, direction * maxSpeed))
				rb.AddForce(direction * acceleration);

			if (HasReachedWaypoint())
				currentWaypoint++;
		}
	}
}
