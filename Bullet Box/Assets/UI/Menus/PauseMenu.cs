using BulletBox.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

//Originally from AssetFactory
namespace BulletBox
{
    public class PauseMenu : MenuManager
	{
		public static PauseMenu Inst { get; private set; }
		public static bool IsPaused => Inst.Paused;

		private bool paused;
		public bool Paused
		{
			get => paused;
			set
			{
				paused = value;
				if (value)
				{
					ToMain();
				}
				else
				{
					if (currentMenu != null)
						currentMenu.Display(false);
				}

				Time.timeScale = value ? 0f : 1f;
			}
		}

		public void TogglePause()
		{
			if (!isActiveAndEnabled)
				return;
			//Unpause only if in main screen
			if (Paused && navigationStack.Count > 0)
				return;

			Paused = !Paused;
		}
		
		public override void Back()
		{
			//Delay to avoid unpausing instead of backing out
			StartCoroutine(BackCoroutine());
		}
		IEnumerator BackCoroutine()
		{
			yield return null;
			base.Back();
		}

		private void Awake()
		{
			if (Inst == null)
			{
				Inst = this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Debug.LogWarning($"Singleton for {typeof(PauseMenu)} already exists.");
				Destroy(gameObject);
			}
		}

		//protected override void OnEnable()
		//{
		//	pauseAction.performed += _ => TogglePause();
		//	base.OnEnable();
		//}
		//protected override void OnDisable()
		//{
		//	if (CurrentMenu != null)
		//		CurrentMenu.Display(false);
		//	pauseAction.performed -= _ => TogglePause();
		//	base.OnDisable();
		//}
	}
}
