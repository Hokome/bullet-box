using System.Collections.Generic;
using UnityEngine;

//Originally from AssetFactory
namespace BulletBox
{
	public static class Utility
	{
		public static void CopyToClipboard(string text)
		{
			GUIUtility.systemCopyBuffer = text;
		}
		public static bool IsInLayerMask(int layer, LayerMask mask) => (mask.value & (1 << layer)) > 0;

		public static Vector2 RandomSquare(float radius)
			=> new Vector2(
				Random.Range(-radius, radius),
				Random.Range(-radius, radius));
		public static Vector2 RandomCircle(float radius)
		{
			Vector2 v;
			do
			{
				v = RandomSquare(1f);
			} while (v.sqrMagnitude > 1f);
			return v * radius;
		}

		/// <summary>
		/// Selects a random element in the list
		/// </summary>
		public static T GetRandom<T>(this IList<T> list)
		{
			int r = UnityEngine.Random.Range(0, list.Count);
			return list[r];
		}

#nullable enable
		/// <summary>
		/// Selects a random element in the list. 
		/// If the element doesn't satisfy the condition, it will select an other random element.
		/// Not infinite loop safe
		/// </summary>
		/// <param name="eligible">Condition for selection</param>
		public static T SelectRandom<T>(this IList<T> list, System.Func<T, bool> eligible)
		{
			T choice;
			do
				choice = list[Random.Range(0, list.Count)];
			while (!eligible(choice));
			return choice;
		}
		/// <summary>
		/// Selects a random element in the list.
		/// If the element doesn't satisfy the condition, selects the next one.
		/// Not equal probablility distribution, but prevents infinite loops.
		/// </summary>
		/// <param name="eligible">Condition for selection</param>
		/// <returns></returns>
		public static T SelectRandomSafe<T>(this IList<T> list, System.Func<T, bool> eligible)
		{
			int start = Random.Range(0, list.Count);
			int next = start;
			T choice = list[start];
			while (!eligible(choice))
			{
				next = (next + 1) % list.Count;
				if (start == next)
				{
					Debug.LogWarning("No element was eligible for selection, returning random element");
					break;
				}
				choice = list[next];
			}
			return choice;
		}
#nullable disable

		#region Visual
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
		#endregion
	}
}