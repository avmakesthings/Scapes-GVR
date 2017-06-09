using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioNoodles : MonoBehaviour {

	public GameObject sampleNoodlePrefab;
	GameObject[] sampleNoodle = new GameObject[512];
	public float maxScale;
	// Use this for initialization
	void Start () {
		for (int i=0; i<512;i++){
			GameObject instanceNoodle = (GameObject)Instantiate(sampleNoodlePrefab);
			instanceNoodle.transform.position = this.transform.position;
			instanceNoodle.transform.parent = this.transform;
			instanceNoodle.name = "Noodle" + i;
			this.transform.eulerAngles = new Vector3(0,-0.703125f*i,0);
			instanceNoodle.transform.position = Vector3.forward*.1f;
			sampleNoodle[i] = instanceNoodle;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for(int i =0; i<512; i++){
			if(sampleNoodle != null){
				sampleNoodle[i].transform.localScale = new Vector3(1,AudioPeer.samples[i]*maxScale,1);
			}
		}
	}
}
