using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

//Originally from AssetFactory
namespace BulletBox
{
	public class SceneTransitioner : MonoSingleton<SceneTransitioner>
	{
		[SerializeField] private UnityEvent<float> onProgress;
		protected RectTransform canvasRect;

		protected override void Awake()
		{
			DontDestroyOnLoad(gameObject);
			base.Awake();
		}

		private void Start()
		{
			canvasRect = (RectTransform)transform;
		}

		public virtual void LoadScene(int index)
		{
			LoadScene(index, delegate { });
		}
		public virtual void LoadScene(int index, Action doneLoading)
		{
			LTDescr startAnim = StartAnimation();
			if (startAnim == null)
			{
				LoadScene(index);
				doneLoading.Invoke();
				EndAnimation();
				return;
			}
			onProgress.Invoke(0);
			startAnim.setOnComplete(() =>
			{
				AsyncOperation operation = SceneManager.LoadSceneAsync(index);
				operation.completed += delegate
				{
					doneLoading.Invoke();
					EndAnimation();
				};
				StartCoroutine(LoadingCheck(operation));
			});
		}

		private IEnumerator LoadingCheck(AsyncOperation loading)
		{
			while (!loading.isDone)
			{
				onProgress.Invoke(loading.progress);
				yield return null;
			}
			onProgress.Invoke(1f);
		}

		protected virtual LTDescr StartAnimation() { return null; }
		protected virtual void EndAnimation() { }
	}
}