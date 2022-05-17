using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class SpawnZone : MonoBehaviour
    {
		[SerializeField] private Vector2 size;
		
		private Vector2 HalfSize => size * 0.5f;

		public Vector2 GetPosition()
		{
			Vector2 hs = HalfSize;
			Vector2 pos = transform.position;
			return new Vector2(RandCoord(pos.x, hs.x), RandCoord(pos.y, hs.y));
		}

		private float RandCoord(float m, float hs) => Random.Range(m - hs, m + hs);

		private void OnDrawGizmosSelected()
		{
			Vector2 hs = HalfSize;
			Vector2 pos = transform.position;
			Rect bounds = new Rect(pos - hs, size);
			DebugEx.DrawRect(bounds, Color.blue);
		}
	}
}
