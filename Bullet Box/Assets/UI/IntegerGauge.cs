using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace BulletBox.UI
{
	public class IntegerGauge : MonoBehaviour
	{
		[SerializeField] private int value;
		[SerializeField] private int max;
		[SerializeField] private Sprite on;
		[SerializeField] private Sprite off;
		[SerializeField] private Image prefab;
		
		private List<Image> images;
		private LayoutGroup lg;

		public int Value
		{
			get => value;
			set
			{
				this.value = Mathf.Min(value, max);
				UpdateDisplay();
			}
		}
		public int Max
		{
			get => max;
			set
			{
				max = value;
				Generate();
			}
		}
		private LayoutGroup Group
		{
			get
			{
				if (lg == null)
					lg = GetComponent<LayoutGroup>();
				return lg;
			}
		}
		private void Start()
		{
			Generate();
		}

		private void Generate()
		{
			if (images != null)
			{
				for (int i = 0; i < images.Count; i++)
				{
					Destroy(images[i].gameObject);
				}
			}
			images = new List<Image>(max);
			for (int i = 0; i < max; i++)
			{
				images.Add(Instantiate(prefab, transform));
			}
			Group.CalculateLayoutInputHorizontal();
			Group.CalculateLayoutInputVertical();
			UpdateDisplay();
		}
		private void UpdateDisplay()
		{
			int i = 0;
			while (i < value)
			{
				images[i].sprite = on;
				i++;
			}
			while (i < max)
			{
				images[i].sprite = off;
				i++;
			}
		}
	}
}
