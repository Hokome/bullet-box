using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class MapDisplayer : MonoBehaviour
    {
		[SerializeField] private Color dotColor = Color.white;

		private void Start()
		{
			SpriteRenderer sr = Instantiate(MapManager.Inst.MapDot, transform);
			sr.color = dotColor;
			sr.transform.localPosition = Vector3.forward * MapManager.Inst.MapOffset;
		}
	}
}
