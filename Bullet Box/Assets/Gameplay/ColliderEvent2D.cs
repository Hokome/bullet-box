using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BulletBox
{
	[RequireComponent(typeof(Collider2D))]
	public class ColliderEvent2D : MonoBehaviour
    {
		public bool trigger;
		public UnityEvent<Collider2D> onEnter;
		public UnityEvent<Collider2D> onExit;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (trigger)
				onEnter.Invoke(collision);
		}
		private void OnTriggerExit2D(Collider2D collision)
		{
			if (trigger)
				onExit.Invoke(collision);
		}
		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (!trigger)
				onEnter.Invoke(collision.collider);
		}
		private void OnCollisionExit2D(Collision2D collision)
		{
			if (!trigger)
				onExit.Invoke(collision.collider);
		}
	}
}
