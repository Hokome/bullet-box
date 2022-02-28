using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Originally from AssetFactory
namespace AssetFactory.UI
{
	public class DoubleGauge : MonoBehaviour
	{
		[SerializeField] private float min = 0f;
		[SerializeField] private float max = 1f;
		[SerializeField] private float value = 0.2f;
		[SerializeField] private float subValue = 0.4f;
		[Header("Display")]
		[SerializeField] private Color mainColor;
		[SerializeField] private Color subIncreaseColor;
		[SerializeField] private Color subDecreaseColor;
		[SerializeField] private float animationTime;
		[Header("References")]
		[SerializeField] private Slider front;
		[SerializeField] private Slider back;
		[SerializeField] private Image frontFill;
		[SerializeField] private Image backFill;

		private Slider sub;

		public float Value
		{
			get => value;
			set
			{
				ForceUpdate();
				this.value = value;
				anim = StartCoroutine(Animate());
			}
		}
		public float SubValue
		{
			get => subValue;
			set
			{
				subValue = value;
				sub.value = value;
			}
		}

		public float Min
		{
			get => min;
			set
			{
				min = value;
				front.minValue = value;
				back.minValue = value;
			}
		}
		public float Max
		{
			get => max;
			set
			{
				max = value;
				front.maxValue = value;
				back.maxValue = value;
			}
		}

		private void Start() => Init();
		private void OnValidate() => Init();
		private void Init()
		{
			if (front != null)
				front.value = value;
			if (back != null)
				back.value = subValue;
			
			Min = min;
			Max = max;
			
			if (frontFill != null)
				frontFill.color = mainColor;
			if (backFill != null)
				backFill.color = subDecreaseColor;
		}
		public void ForceUpdate()
		{
			if (anim != null)
				StopCoroutine(anim);
			subValue = value;
			front.value = value;
			back.value = value;
		}

		private Coroutine anim;
		private IEnumerator Animate()
		{
			float increment = value - subValue / animationTime;
			if (value > subValue)
			{
				back.value = value;
				sub = front;
				backFill.color = subIncreaseColor;
				while (subValue < value)
				{
					SubValue += increment * Time.deltaTime;
					yield return null;
				}
			}
			else if (value < subValue)
			{
				front.value = value;
				sub = back;
				backFill.color = subDecreaseColor;
				while (subValue > value)
				{
					SubValue += increment * Time.deltaTime;
					yield return null;
				}
			}
		}
	}
}
