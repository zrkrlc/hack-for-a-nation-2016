using UnityEngine;
using System.Collections;
using Interlude;

namespace Interlude {
	public class IdentityController : MonoBehaviour {

		private SteamVR_TrackedObject trackedObj;

		// Fields for draw-on-air
		[SerializeField] Material lineMaterial;
		[SerializeField] float lineWidth = 0.025f;
		private GraphicsLineRenderer lineCurrent;
		private int countClicks;
		private GameObject drawnLines;

		void Start () {
			trackedObj = GetComponent<SteamVR_TrackedObject> ();

			// Instantiates an empty GameObject to hold lines
			drawnLines = new GameObject("Drawn Lines");
		}

		void Update () {
			SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);

			// Enables draw-on-air
			if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
				GameObject line = new GameObject ();
				line.transform.parent = drawnLines.transform;
				line.AddComponent<MeshFilter> ();
				line.AddComponent<MeshRenderer> ();
				lineCurrent = line.AddComponent<GraphicsLineRenderer> ();

				lineCurrent.SetWidth (lineWidth);

				lineCurrent.lineMaterial = lineMaterial;
			} else if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
				lineCurrent.AddPoint (trackedObj.transform.position);
			}

			// Enables 
		}





	}
}
