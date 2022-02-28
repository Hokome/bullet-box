using UnityEngine;

//Originally from AssetFactory
namespace AssetFactory
{
	public class AnimationManager
	{
		[HideInInspector] public Animator a;
		public string CurrentAnimation { get; private set; } = "";

		public void ChangeAnim(string newAnimation)
		{
			if (newAnimation == CurrentAnimation)
				return;

			a.Play(newAnimation, 0);

			CurrentAnimation = newAnimation;
		}

		public AnimationClip GetClip(string name)
		{
			foreach (AnimationClip clip in a.runtimeAnimatorController.animationClips)
			{
				if (clip.name == name)
				{
					return clip;
				}
			}
			Debug.LogError($"Animation clip with name '{name}' not found");
			return null;
		}
	}
}