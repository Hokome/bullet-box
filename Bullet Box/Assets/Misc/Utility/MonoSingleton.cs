using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Originally from AssetFactory
namespace BulletBox
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
		private static T inst;
		public static T Inst
		{
			get
			{
				if (Exists) return inst;
				Debug.LogError($"No instance of the singleton type {typeof(T)} has been registered.");
				return null;
			}
		}
		public static bool Exists => inst != null;

		private static T FindInstance()
		{
			return FindObjectOfType<T>();
		}
		private static T ForceCreateInstance()
		{
			Debug.Log($"An instance of the singleton type {typeof(T)} was created. It may not be initialized properly.");
			return new GameObject(typeof(T).Name).AddComponent<T>();
		}

		protected virtual void Awake()
		{
			if (inst == null)
			{
				inst = (T)this;
			}
			else
			{
				Debug.LogWarning($"Singleton for {typeof(T)} already exists.");
				Destroy(gameObject);
			}
		}
	}
}
