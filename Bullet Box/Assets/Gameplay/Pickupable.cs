using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
	[RequireComponent(typeof(Collider2D))]
	public abstract class Pickupable : Spawnable
    {
		public abstract void PickUp(Player p);

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.CompareTag("Player"))
			{
				Player.Inst.NotifyPickupable(this, true);
			}
		}
		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.CompareTag("Player"))
			{
				Player.Inst.NotifyPickupable(this, false);
			}
		}
	}
}
