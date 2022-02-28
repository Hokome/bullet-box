using UnityEngine;
using UnityEngine.SceneManagement;

//Originally from AssetFactory
namespace BulletBox.Initialization
{
	public class SetupFinish : MonoBehaviour
    {
		public static bool HasInitialized { get; private set; }

		private void Start()
		{
			HasInitialized = true;
			SceneManager.LoadScene(1);

			AudioManager.Inst.UpdateMixers();
		}
	}
}
