using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BulletBox
{
	public class Crosshair : MonoBehaviour
    {
		[SerializeField] private float distance;
		public float Distance => distance;
	}
}
