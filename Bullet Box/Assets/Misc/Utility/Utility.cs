using UnityEngine;

//Originally from AssetFactory
namespace AssetFactory
{
	public static class Utility
	{
		public static void CopyToClipboard(string text)
		{
			GUIUtility.systemCopyBuffer = text;
		}
		public static bool IsInLayerMask(int layer, LayerMask mask) => (mask.value & (1 << layer)) > 0;
		public static Color ChangeAlpha(Color baseColor, float newAlpha)
		{
			baseColor.a = newAlpha;
			return baseColor;
		}
		/// <summary>
		/// Shows or hides a canvas group.
		/// </summary>
		/// <param name="group">Canvas group to show or hide.</param>
		/// <param name="v">Whether or not to show the canvas group.</param>
		/// <param name="interactable">If false, the canvas will not block raycasts or be interactable.</param>
		public static void ShowCanvas(CanvasGroup group, bool v, bool interactable = true)
		{
			group.alpha = v ? 1f : 0f;
			group.interactable = v && interactable;
			group.blocksRaycasts = v && interactable;
		}

		public static void SetAnchors(RectTransform rectTransform, Vector2 anchors)
		{
			rectTransform.anchorMin = anchors;
			rectTransform.anchorMax = anchors;
		}
		public static void SetAnchorX(RectTransform rectTransform, float anchorX)
		{
			rectTransform.anchorMin = new Vector2(anchorX, rectTransform.anchorMin.y);
			rectTransform.anchorMax = new Vector2(anchorX, rectTransform.anchorMax.y);
		}
		public static void SetAnchorY(RectTransform rectTransform, float anchorY)
		{
			rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.x, anchorY);
			rectTransform.anchorMax = new Vector2(rectTransform.anchorMin.x, anchorY);
		}
	}
}