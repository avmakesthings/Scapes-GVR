using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyAudioParticles : MonoBehaviour {

	public ParticleSystem[] myParticle;

	Vector3 controlGeoCenter;

	ParticleSystem[] a = new ParticleSystem[3];

	void Awake()
	{
		//controlGeoCenter = Camera.main.transform.position + new Vector3(0,0,20);
		controlGeoCenter = GetComponent<Transform>().position;
		a[0] = (ParticleSystem)Instantiate(myParticle[0], controlGeoCenter, Quaternion.identity);
		a[0].transform.parent = gameObject.transform;
		a[1] = (ParticleSystem)Instantiate(myParticle[1], nextPoint(a[0].transform.position), Quaternion.identity);
		a[1].transform.parent = gameObject.transform;
		a[2] = (ParticleSystem)Instantiate(myParticle[2], nextPoint(a[1].transform.position), Quaternion.identity);
		a[2].transform.parent = gameObject.transform;
	}


	
	// Update is called once per frame
	void Update () {
		//Update base position of transform 
		
		float audioInput = AudioPeer.audioBandBuffer[1];

		if(audioInput>0.7f && audioInput<0.9f ){
			a[0].transform.position = nextPoint(a[2].transform.position);
		}

		if(audioInput<0.7f && audioInput>0.6f){
			a[1].transform.position = nextPoint(a[0].transform.position);
		}

		if(audioInput<0.5f && audioInput>0.3f){
			a[2].transform.position = nextPoint(a[1].transform.position);
		}


	}

	
    private static Vector3 RandomPointInBox(Vector3 center, Vector3 size) {
 
		return center + new Vector3(
			(Random.value - 0.5f) * size.x,
			(Random.value - 0.5f) * size.y,
			(Random.value - 0.5f) * size.z
		);
	}
	

	Vector3 nextPoint(Vector3 currentPoint){
		return RandomPointInBox(controlGeoCenter, new Vector3 (0,20,20));
	}

}
