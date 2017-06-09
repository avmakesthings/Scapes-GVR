using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateLogo : MonoBehaviour {

	// Update is called once per frame
	void Start () {
		StartCoroutine ("animateTexture");
	}

	private IEnumerator animateTexture () {
		while (true) {
			MeshRenderer obj;
			obj = GetComponent<MeshRenderer> ();

			obj.material.mainTextureOffset = new Vector2 (Random.Range (0f, .1f), Random.Range (0f, .1f));
			yield return new WaitForSeconds (.1f);
		}
	}
}
