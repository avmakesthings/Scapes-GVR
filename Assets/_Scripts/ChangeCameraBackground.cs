using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ChangeCameraBackground : MonoBehaviour {


	Camera myCamera;

	public Color[] myColors;

	// Use this for initialization
	void Start () {
		myCamera = GetComponent<Camera>();
	}
	

	public void changeBackgroundColor(int index){
		if (index>myColors.Length){
			print("smalled value");
		}else{
			myCamera.backgroundColor = myColors[index];
		}
	}

}
