using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {
	public float orbitSpeed = 20;

	void Update() {
		// Rotate the object every frame so it keeps looking at the origin 
		transform.LookAt(Vector3.zero);

		// Orbit the object around the origin
		transform.RotateAround(Vector3.zero, Vector3.up, orbitSpeed * Time.deltaTime);
	}
}
