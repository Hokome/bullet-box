using System.IO;
using UnityEngine;

//Originally from AssetFactory
namespace AssetFactory.Initialization
{
	public class SaveInit : MonoBehaviour
    {
		private void Start()
		{
			Directory.CreateDirectory($@"{Application.dataPath}/Save");
		}
	}
}
