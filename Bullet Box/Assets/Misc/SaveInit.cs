using System.IO;
using UnityEngine;

//Originally from AssetFactory
namespace BulletBox.Initialization
{
	public class SaveInit : MonoBehaviour
    {
		private void Start()
		{
			Directory.CreateDirectory($@"{Application.dataPath}/Save");
		}
	}
}
