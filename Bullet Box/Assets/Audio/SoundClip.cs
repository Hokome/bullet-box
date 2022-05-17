using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//Originally from AssetFactory
namespace BulletBox.Audio
{
	public abstract class SoundClip : ScriptableObject
	{
		public AudioClip clip;
		[Space]
		[Range(0f,5f)] public float volume = 1f;
		[Range(0.1f, 3f)] public float pitch = 1f;
		[Space]
		public AudioMixerGroup channel;

		public abstract SoundType Type { get; }

		public virtual AudioSource CreateSource(GameObject obj, PlayOptions options)
		{
			AudioSource source = obj.AddComponent<AudioSource>();
			source.clip = clip;
			source.volume = volume * options.volume;
			source.pitch = pitch * options.pitch;
			source.loop = options.loop;
			source.outputAudioMixerGroup = channel;
			return source;
		}
	}

	public enum SoundType
	{
		SFX,
		Music,
		Voice
	}
}