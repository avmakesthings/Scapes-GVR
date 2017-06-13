using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMorph : MonoBehaviour {

	string[] basePositionCSVS; 
	Vector3[,] basePositions; //Vector3 positions from surface points
	Vector3[] currentPositions; 

	GameObject[] cubes;
	int positionIndex;

	int positionVectorCount;
	
	// Use this for initialization
	void Start () {
		
		getBasePositionsFromCSVS();

		positionIndex = 0;
		positionVectorCount = basePositions.GetLength(1);
		currentPositions = new Vector3[positionVectorCount];

		for (int i= 0; i<positionVectorCount; i++){
			currentPositions[i] = basePositions[0,i]; 
		}
		createCubes(currentPositions);
		setCubePosition(currentPositions);
	}

	// Update is called once per frame
	void Update () {
		
		float audioInput = AudioPeer.audioBandBuffer[5];

		if(audioInput>0.9f && audioInput<1f ){
			setNextPostitions();
		}


		setCubePosition(currentPositions);
	}



	void getBasePositionsFromCSVS() {
		
		basePositionCSVS = new string[] 
			{"Points/points-surface-0",
			"Points/points-surface-1",
			"Points/points-surface-2", 
			"Points/points-surface-3"  
			};
		
		for(int i=0; i<basePositionCSVS.Length; i++){
			
			TextAsset pointsCSV = Resources.Load(basePositionCSVS[i]) as TextAsset;

			CSVParsing c = new CSVParsing(pointsCSV, 3);
			string[,] pointsStrings = c.readData();

			if (i==0){
				basePositions = new Vector3[basePositionCSVS.Length,pointsStrings.GetLength(0)];	
			}

			//TO-DO - ending up with nulls in the array - need to add some better handl ing
			for(int j=0; j<pointsStrings.GetLength(0); j++){
				basePositions[i,j] = new Vector3(float.Parse(pointsStrings[j,0]),float.Parse(pointsStrings[j,1]),float.Parse(pointsStrings[j,2]));
			}
		}

	}

	void setNextPostitions(){
		if(positionIndex==3){
			positionIndex =0;
		}
		else{
			positionIndex++;
		}
		
		for(int i = 0; i<positionVectorCount; i++){
			currentPositions[i] = basePositions[positionIndex,i]; 
		}

	}

	GameObject[] createCubes(Vector3[] position){
		
		cubes = new GameObject[position.Length];
		
		for(int i =0; i<position.Length; i++){
			cubes[i]=GameObject.CreatePrimitive(PrimitiveType.Cube);
		}
		return cubes;
	}

	void setCubePosition(Vector3[] position) {

		for(int i =0; i<position.Length; i++){
			cubes[i].transform.position = position[i];
		}
	}



}
