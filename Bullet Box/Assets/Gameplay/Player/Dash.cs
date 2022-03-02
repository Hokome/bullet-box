using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{

	[CreateAssetMenu(fileName = "Dash", menuName = "Ability/Dash", order = 1)]
    public class Dash : Ability
    {
		[SerializeField] private float force;

		public override bool Use(Player p)
		{
			Vector2 f = p.MoveInput.normalized;
			if (f.sqrMagnitude == 0f)
				return false;
			
			p.Rb.AddForce(f * force, ForceMode2D.Impulse);
			return true;
		}
    }
}
