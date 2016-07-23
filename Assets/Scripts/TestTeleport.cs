/* File: NewBehaviourScript.cs
 * Description: #DESCRIPTION#
 * How to use: #INSTRUCTIONS#
*/

using UnityEngine;
using System.Collections;
using Interlude;


namespace Interlude {
	public class TestTeleport : MonoBehaviour {

		public Vector3 targetLocation;
		public float radius;

		void Start () {
		
		}
		
		void Update () {
			Vector3 currentPosition = this.GetComponent<Transform> ().position;
			Vector3 distanceFromTarget = targetLocation - currentPosition;

			if (distanceFromTarget.magnitude >= radius) {
				GetComponent<Rigidbody> ().velocity = distanceFromTarget.normalized * 2f;
			}
			if (distanceFromTarget.magnitude < radius) {
				GetComponent<Rigidbody> ().velocity = Vector3.zero;
			}
		}
	}
}
