using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public abstract class Ability : ScriptableObject
    {
		[SerializeField] private float cooldown;
		public float Cooldown => cooldown;

		public abstract bool Use(Player p);
	}
}
