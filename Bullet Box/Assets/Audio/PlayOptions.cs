using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Originally from AssetFactory
namespace BulletBox.Audio
{
	[System.Serializable]
    public class PlayOptions 
	{
		public PlayOptions()
		{
			volumeMultiplier = 1f;
			pitchMultiplier = 1f;
			loop = false;
		}

		public float volumeMultiplier = 1f;
		public float pitchMultiplier = 1f;
		public float maxDistance = 0f;
		public bool loop = false;
    }
}
