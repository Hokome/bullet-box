using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//Originally from AssetFactory
namespace BulletBox.UI
{
	[RequireComponent(typeof(CanvasGroup))]
	public class Menu : MonoBehaviour
    {
		[SerializeField] private bool startDisplayed;
		[SerializeField] private GameObject selection;
		[SerializeField] protected UnityEvent onDisplay;
		public CanvasGroup Group { get; private set; }

		private void Awake()
		{
			Group = GetComponent<CanvasGroup>();
			Display(startDisplayed);
		}

		public virtual void Display(bool value)
		{
			Utility.ShowCanvas(Group, value);
			if (value)
			{
				EventSystem.current.SetSelectedGameObject(selection);
				onDisplay.Invoke();
			}
		}
	}
}
