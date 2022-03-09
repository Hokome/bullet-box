using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BulletBox.UI
{
    public class ScoreViewer : MonoBehaviour
    {
		[SerializeField] private TMP_Text scorePrefab;
		[SerializeField] private RectTransform arcadeContentTransform;
		[SerializeField] private RectTransform freeplayContentTransform;

		public void InitializeScores()
		{
			for (int i = 0; i < arcadeContentTransform.childCount; i++)
			{
				Destroy(arcadeContentTransform.GetChild(i).gameObject);
			}
			for (int i = 0; i < freeplayContentTransform.childCount; i++)
			{
				Destroy(freeplayContentTransform.GetChild(i).gameObject);
			}

			Save.Current.SortScores();

			foreach (var span in Save.Current.arcadeScores)
			{
				Instantiate(scorePrefab, arcadeContentTransform).text =
					$"{span.Minutes:00}:{span.Seconds:00}.{span.Milliseconds:000}";
			}
			foreach (var span in Save.Current.freeplayScores)
			{
				Instantiate(scorePrefab, freeplayContentTransform).text =
					$"{span.Minutes:00}:{span.Seconds:00}.{span.Milliseconds:000}";
			}
		}
	}
}
