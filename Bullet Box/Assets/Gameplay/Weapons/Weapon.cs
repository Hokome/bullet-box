using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public abstract class Weapon : MonoBehaviour
    {
		[SerializeField] private SpriteRenderer sprite;
		public SpriteRenderer Sr => sprite;

		public abstract bool Fire();
    }
}
