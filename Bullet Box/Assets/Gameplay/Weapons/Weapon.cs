using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public abstract class Weapon : MonoBehaviour
    {
		[SerializeField] private string id;
		[SerializeField] private SpriteRenderer sprite;
		public SpriteRenderer Sr => sprite;
		public string ID => id;

		public abstract bool Fire();
	}
}
