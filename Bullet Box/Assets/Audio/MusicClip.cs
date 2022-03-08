using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//Originally from AssetFactory
namespace BulletBox.Audio
{

	[CreateAssetMenu(fileName = "music_track", menuName = "Audio/Music", order = 215)]
    public class MusicClip : SoundClip
    {
		public override SoundType Type => SoundType.Music;
	}
}
