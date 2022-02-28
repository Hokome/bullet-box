using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Originally from AssetFactory
namespace AssetFactory.Initialization
{
    public class SettingsInit : MonoBehaviour
    {
		private void Start()
		{
			Settings.current = new Settings();
			Settings.current.SetScreen();
		}
	}
}
