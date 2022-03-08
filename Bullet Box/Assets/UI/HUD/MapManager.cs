using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class MapManager : MonoSingleton<MapManager>
    {
		[SerializeField] private SpriteRenderer mapDot;

		public SpriteRenderer MapDot => mapDot;
		public float MapOffset => transform.position.z - 1;
	}
}
