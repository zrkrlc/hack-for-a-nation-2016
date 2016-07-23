using UnityEngine;
using System.Collections;

public class SpawnSlab : MonoBehaviour {

	public GameObject prefabSlab;
	public Transform spawnPoint;

	public void spawnSlab (Texture2D texturePhoto) {
		var objectSlab = (GameObject)Instantiate (prefabSlab, spawnPoint.position, spawnPoint.rotation);
		var objectScreen = objectSlab.GetComponent<Transform>().FindChild ("Screen").gameObject;
		objectScreen.GetComponent<Renderer>().material.mainTexture = texturePhoto;
	}
}
