using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinImportedMesh : MonoBehaviour {

	public float speed;
	public Vector3 direction;

	public Renderer rend;
	void Start() {
		rend = GetComponent<Renderer>();
	}

	// Update is called once per frame
	void Update () {
		Vector3 center = rend.bounds.center;
		transform.RotateAround(center, direction, speed);

	}


}
