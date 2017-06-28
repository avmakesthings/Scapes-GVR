using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioParticles : MonoBehaviour {
	
	Transform particleTransform;
	public MeshFilter meshBounds;

	public ParticleSystem myParticle;
	Mesh mesh;

	Vector3 controlGeoCenter;
	Vector3 randomVertex;
	Vector3 randomPointAB;

	Vector3 currentPoint;

	bool isRunning;

	private Vector3 velocity = Vector3.zero;
	
	ParticleSystem a ;

	void Awake()
	{
		currentPoint = new Vector3(0,0,10);
		a = (ParticleSystem)Instantiate(myParticle, currentPoint, Quaternion.identity );
		a.transform.parent = gameObject.transform;
	}

	// Use this for initialization
	void Start () {

		mesh = meshBounds.mesh;
		controlGeoCenter = meshBounds.transform.position;
		currentPoint = a.transform.position;
		findNextPoint();

	}
	
	// Update is called once per frame
	void Update () {
		//Update base position of transform 
		
		float audioInput = AudioPeer.audioBandBuffer[0];
		

		if(audioInput>=1f && audioInput<= 1.1f && !isRunning){
			// assignPoint();
			// findNextPoint();
			StartCoroutine("changePosition");
			print(audioInput);
			
		}
		
				
	}

	
	void findNextPoint(){

		randomVertex = mesh.vertices[Random.Range(0,mesh.vertexCount)]; 
		randomPointAB = Vector3.Lerp(randomVertex, controlGeoCenter, Random.Range(0,1));
		//randomPointAB = Vector3.Lerp(randomPointAB, currentPoint,.5f);

	}
	
	void assignPoint(){
		
		transform.position = Vector3.SmoothDamp(randomPointAB, currentPoint , ref velocity, 200f);
		//a.transform.position = randomPointAB;
		currentPoint = randomPointAB;
	}

	IEnumerator changePosition() {
		isRunning = true;
		assignPoint();
		findNextPoint();
		yield return new WaitForSeconds(10f);
		isRunning = false;
	}

}
