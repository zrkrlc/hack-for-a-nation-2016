
/* File: DumpRenderTextureData.cs
 * How to use: 
 * 1) Attach to a camera.
 * 2) Attach to this script a target RenderTexture, if need be.
*/

using UnityEngine;
using System.Collections;
using System.IO;

public class DumpRenderTextureData : MonoBehaviour {

	public Renderer rendererDisplay;
	public bool isTakingPicture = false;

	private RenderTexture rtSource;
	private RenderTexture rtOriginal;
	private Texture2D texturePhoto;

	private Vector2 vectorTargetDimensions;
	private string pathDumpDirectory = "Gallery";

	void Start() {
		rtSource = gameObject.GetComponent<Camera> ().targetTexture;
		vectorTargetDimensions = new Vector2 (256, 256);
			
	}

	void LateUpdate () {
		if (isTakingPicture) {
			// Stores original rt
			rtOriginal = RenderTexture.active;

			// Replaces active rt with rtSource
			// Note that the default target of this part of the
			// script is the phone's screen
			RenderTexture.active = rtSource;

			texturePhoto = new Texture2D ((int)vectorTargetDimensions.x, (int)vectorTargetDimensions.y);
			texturePhoto.ReadPixels (new Rect (0, 0, vectorTargetDimensions.x, vectorTargetDimensions.y), 0, 0);
			texturePhoto.Apply ();

			// Dump photo to a file
			DumpToPNG(texturePhoto);

			// Checks if RenderTexture has to be rendered on a display
			if (rendererDisplay != null) {
				rendererDisplay.material.mainTexture = texturePhoto;
			}

			// Restores original rt after a short delay
			StartCoroutine(RestoreScreenWithDelay(1.0f));

			// Spawns a photo slab
			GetComponent<SpawnSlab> ().spawnSlab(texturePhoto);

			// Resets camera state
			isTakingPicture = false;
			}
		}

	void DumpToPNG (Texture2D texturePhoto) {
		EnsurePathExists ();
		byte[] bytes = texturePhoto.EncodeToPNG ();
		var pathSave = Application.dataPath + "/Gallery/" + GetNewName ();

		File.WriteAllBytes (pathSave, bytes);
		Debug.Log ("Photo saved as " + pathSave + ".");
	}

	void EnsurePathExists() {
		var path = Path.Combine(Application.dataPath, pathDumpDirectory);

		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
	}

	private static string GetNewName()
	{
		return string.Format(
			"{0}_{1}.png",
			Application.loadedLevelName,
			System.DateTime.Now.ToString(
				"yyyy-MM-dd_HH-mm-ss"));
	}

	// Implements a delay
	IEnumerator RestoreScreenWithDelay(float seconds) {
		yield return new WaitForSeconds (seconds);
		RenderTexture.active = rtOriginal;
	}
		
}
