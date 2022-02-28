using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Originally from AssetFactory
namespace AssetFactory
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
		private static T inst;
		public static T Inst
		{
			get
			{
				if (inst != null) return inst;
				Debug.Log($"{inst.name}");
				Debug.LogError($"No instance of the singleton type {typeof(T)} has been registered.");
				return null;
			}
		}

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
