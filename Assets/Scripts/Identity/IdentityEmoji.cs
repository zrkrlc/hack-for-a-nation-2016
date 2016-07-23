/* File: IdentityEmoji.cs
 * Description: #DESCRIPTION#
 * How to use: attach to emoji manager.
*/

using UnityEngine;
using System.Collections;
using Interlude;


namespace Interlude {
	public class IdentityEmoji : MonoBehaviour {
		
		[SerializeField] GameObject prefabEmoji;
		[SerializeField] Transform transformPlayer;
		[SerializeField] float durationPopup;

		void Update() {
			if (Input.GetKeyDown (KeyCode.E)) {
				Debug.Log ("IdentityEmoji.cs: spawning emoji " + prefabEmoji.name + "...");
				StartCoroutine (Popup (prefabEmoji, durationPopup));
			}
		}

		// TODO: make emoji stay vertical
		IEnumerator Popup (GameObject _prefabEmoji, float seconds) {
			GameObject _emoji = (GameObject)Instantiate (
				_prefabEmoji, 
				transformPlayer.position + Vector3.up * 0.75f, 
				Quaternion.LookRotation(-transformPlayer.forward));
			_emoji.transform.parent = this.transform;

			// Moves emoji upwards
			Rigidbody rb = _emoji.AddComponent<Rigidbody> ();
			rb.useGravity = false;
			rb.velocity = Vector3.up * durationPopup;

			yield return new WaitForSeconds(seconds);
			Destroy (_emoji);
		}

	}
}
