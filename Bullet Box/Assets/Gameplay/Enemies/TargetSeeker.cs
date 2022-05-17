using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox.AI
{
	[RequireComponent(typeof(Seeker))]
	[RequireComponent(typeof(AIDestinationSetter))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class TargetSeeker : MonoBehaviour
	{
		[SerializeField] protected float acceleration;
		[SerializeField] protected float maxSpeed;
		[SerializeField] protected float nextWaypointDistance = 0.2f;

		protected Rigidbody2D rb;
		protected Seeker seeker;
		protected AIDestinationSetter destination;

		protected Path path;
		protected int currentWaypoint;

		private float sqrNextWaypoint;

		protected bool HasReachedWaypoint()
		{
			Vector2 dir = GetDirection();
			return dir.sqrMagnitude < sqrNextWaypoint;
		}
		protected bool HasReachedEnd() => currentWaypoint >= path.vectorPath.Count;
		protected Vector2 GetDirection() => (Vector2)path.vectorPath[currentWaypoint] - rb.position;
		protected bool PathInvalid() => path == null || HasReachedEnd();

		protected virtual void Start()
		{
			seeker = GetComponent<Seeker>();
			destination = GetComponent<AIDestinationSetter>();
			destination.target = Player.Inst.transform;
			rb = GetComponent<Rigidbody2D>();

			sqrNextWaypoint = nextWaypointDistance * nextWaypointDistance;

			InvokeRepeating(nameof(UpdatePath), 0f, 1f);
		}

		private void OnPathComplete(Path p)
		{
			if (!p.error)
			{
				path = p;
				currentWaypoint = 0;
			}
		}
		private void UpdatePath()
		{
			if (seeker.IsDone())
				seeker.StartPath(rb.position, destination.target.position, OnPathComplete);
		}
	}
}
