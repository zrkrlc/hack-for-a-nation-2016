/* File: IdentityPortalsphere.cs
 * Description: #DESCRIPTION#
 * How to use: note that players must be tagged "Player"
*/

using UnityEngine;
using System.Collections;
using Interlude;


namespace Interlude {
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(Collider))]
	public class IdentityPortalsphere : MonoBehaviour {

		[SerializeField] GameObject prefabEnvironment;
		private GameObject containerEnvironment;

		void Start () {
			// Ensures existence of environment container
			if (!GameObject.Find ("Environment")) {
				containerEnvironment = new GameObject ("Environment");
			} else {
				containerEnvironment = GameObject.Find ("Environment");
			}

			// Places a small-scale copy of the environment inside the portalsphere
			GameObject modelEnvironment = (GameObject)Instantiate (
				                              prefabEnvironment, 
				                              this.transform.position,
				                              this.transform.rotation);
			modelEnvironment.transform.parent = this.transform;
			modelEnvironment.transform.localScale = new Vector3 (0.00125f, 0.00125f, 0.00125f);
			// Ensures that the model environment will not fall
			// through the portalsphere
			Destroy (modelEnvironment.GetComponent<Rigidbody> ());
		}
		
		void OnTriggerEnter(Collider hit) {
			if (hit.tag == "Player") {
				LoadEnvironment (prefabEnvironment);
				Debug.Log ("IdentityPortalsphere.cs: loading " + prefabEnvironment.name + "...");
			}
		}

		// TODO: allow acceptance of scripts for ambient light adjustment
		void LoadEnvironment(GameObject _prefabEnvironment) {
			// Destroys current environment
			foreach (Transform children in containerEnvironment.transform) {
				Destroy (children.gameObject);
			}

			GameObject newEnvironment = (GameObject)Instantiate (_prefabEnvironment, Vector3.zero, Quaternion.identity);
			newEnvironment.transform.parent = containerEnvironment.transform;
		}
			
	}
}
