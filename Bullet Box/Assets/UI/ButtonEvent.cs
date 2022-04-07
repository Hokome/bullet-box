using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BulletBox
{
    public abstract class ButtonEvent<T> : MonoBehaviour
    {
		public T identifier;
		[SerializeField] private UnityEvent<T> eventHandler;

		public void InvokeEvent()
		{
			eventHandler.Invoke(identifier);
		}
    }
}
