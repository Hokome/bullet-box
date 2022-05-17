using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public class HitFlash : MonoBehaviour
    {
		[SerializeField] private Sprite flashSprite;
		[SerializeField] private Color flashColor;
		[SerializeField] private SpriteRenderer baseRenderer;

		private SpriteRenderer flash;

		private void Start()
		{
			if (baseRenderer == null)
				baseRenderer = GetComponent<SpriteRenderer>();

			flash = new GameObject("Flash sprite").AddComponent<SpriteRenderer>();
			flash.transform.parent = transform;
			flash.transform.localPosition = Vector3.zero;

			flash.color = flashColor;
			flash.sortingLayerID = baseRenderer.sortingLayerID;
			flash.sortingOrder = baseRenderer.sortingOrder + 1;
			if (flashSprite != null)
				flash.sprite = flashSprite;
			else
				flash.sprite = baseRenderer.sprite;
			flash.enabled = false;
		}

		public void Flash()
		{
			StartCoroutine(StartFlash());
		}
		private IEnumerator StartFlash()
		{
			flash.enabled = true;
			yield return null;
			yield return new WaitForSeconds(0.02f);
			flash.enabled = false;
		}
	}
}
