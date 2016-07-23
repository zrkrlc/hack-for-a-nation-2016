/* File: TakePicture.cs
 * Description: #DESCRIPTION#
 * How to use: #INSTRUCTIONS#
*/

using UnityEngine;
using System.Collections;
using Interlude;


namespace Interlude {
	public class TakePicture : MonoBehaviour {

		[SerializeField] GameObject objectCamera;
		[SerializeField] GameObject objectManagerCamera;
		[SerializeField] GameObject prefabFlash;

		private DumpRenderTextureData scriptCameraDump;
		private SteamVR_TrackedObject trackedObj;
		private SteamVR_Controller.Device trackedDevice;

		void Start () {
			// Boilerplate for SteamVR
			trackedObj = GetComponent<SteamVR_TrackedObject> ();
			trackedDevice = SteamVR_Controller.Input ((int)trackedObj.index);

			if (objectCamera.GetComponent<DumpRenderTextureData> () == null) {
				Debug.LogError ("TakePicture.cs: phone camera not found.");
			} else {
				scriptCameraDump = objectCamera.GetComponent<DumpRenderTextureData> ();
			}
		}

		void Update () {
			if (trackedDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
				Debug.Log ("TakePicture.cs: taking picture...");
				scriptCameraDump.isTakingPicture = true;
			}

		}
			



	}
}
