/* File: IdentityController.cs
 * Description: #DESCRIPTION#
 * How to use:
 * 1) Make sure DrawnLines layer is at layer 8.
*/

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

		// Fields for eraser
		[SerializeField] float radiusEraser = 0.5f;

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
				line.layer = 8; // DrawnLines layer
				line.AddComponent<MeshFilter> ();
				line.AddComponent<MeshRenderer> ();
				lineCurrent = line.AddComponent<GraphicsLineRenderer> ();

				lineCurrent.SetWidth (lineWidth);

				lineCurrent.lineMaterial = lineMaterial;
			} else if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
				lineCurrent.AddPoint (trackedObj.transform.position);
			}

			// Spawns eraser
			if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Grip)) {
				GameObject eraser = new GameObject ("Eraser");
				eraser.transform.parent = this.transform;
				eraser.transform.localPosition = Vector3.forward;

				SphereCollider colliderEraser = eraser.AddComponent<SphereCollider> ();
				colliderEraser.radius = radiusEraser;
				colliderEraser.isTrigger = true;
				eraser.AddComponent<IdentityEraser> ();



			}
		}





	}
}
