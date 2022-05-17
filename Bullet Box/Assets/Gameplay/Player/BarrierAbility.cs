using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BulletBox
{

	[CreateAssetMenu(fileName = "Barrier", menuName = "Ability/Barrier", order = 1)]
	public class BarrierAbility : Ability
	{
		[SerializeField] private Barrier prefab;
		[SerializeField] private float deploySpeed;
		[SerializeField] private float deployTime;

		public override bool Use(Player p)
		{
			Barrier b = Instantiate(prefab);
			b.Deploy(p.AimInput * deploySpeed, deployTime);
			b.transform.position = p.transform.position;
			b.transform.right = p.AimInput;
			return true;
		}
	}
}
