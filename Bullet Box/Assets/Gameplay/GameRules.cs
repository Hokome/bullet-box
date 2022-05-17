using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{

	[CreateAssetMenu(fileName = "Game rules", menuName = "Game Rules", order = 1)]
    public class GameRules : ScriptableObject
    {
		public Weapon[] availableWeapons;
		[SerializeField] private int maxWeapons;
		public int MaxWeapons => maxWeapons;
	}
}
