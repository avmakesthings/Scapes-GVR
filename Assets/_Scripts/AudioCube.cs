using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCube : MonoBehaviour {

	public int band;
	public float startScale, scaleMultiplier;
	public bool useBuffer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(useBuffer){
			transform.localScale = new Vector3(transform.localScale.x, (AudioPeer
		.bandBuffer[band
		]*scaleMultiplier)+startScale, transform.localScale.z);
		}
		if(!useBuffer){
			transform.localScale = new Vector3(transform.localScale.x, (AudioPeer
		.freqBand[band
		]*scaleMultiplier)+startScale, transform.localScale.z);
		}

	}
}
