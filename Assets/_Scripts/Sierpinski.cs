using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sierpinski : MonoBehaviour {

	public Mesh mesh;
	public Material material;
	public int maxDepth;
	public float size;
	private int depth;


	// Use this for initialization
	void Start () {
		gameObject.AddComponent<MeshRenderer>().material = material;

		if (depth <= maxDepth) {
			// Pull these out & make them globals
			Vector3 p0 = new Vector3(0,0,0);
			Vector3 p1 = new Vector3(1,0,0);
			Vector3 p2 = new Vector3(0.5f,0,Mathf.Sqrt(0.75f));
			Vector3 p3 = new Vector3(0.5f,Mathf.Sqrt(0.75f),Mathf.Sqrt(0.75f)/3);

			new GameObject("Sierpinski Child").
			AddComponent<Sierpinski>().Initialize(this, size/2f*p0);

			new GameObject("Sierpinski Child").
			AddComponent<Sierpinski>().Initialize(this, size/2f*p1);

			new GameObject("Sierpinski Child").
			AddComponent<Sierpinski>().Initialize(this, size/2f*p2);

			new GameObject("Sierpinski Child").
			AddComponent<Sierpinski>().Initialize(this, size/2f*p3);

		}


//		makeSierpinski (1f, 1);
	}


	private void Initialize (Sierpinski parent, Vector3 localTransform) {
//		mesh = parent.mesh;
		gameObject.AddComponent<MeshFilter>().mesh = mesh = new Mesh();
		material = parent.material;
		maxDepth = parent.maxDepth;
		depth = parent.depth + 1;
		size = parent.size / 2f;
		transform.parent = parent.transform;
		transform.localPosition = localTransform;

		if (depth == maxDepth) {



			makeTetrahedron (size, new Vector3 (0, 0, 0));
		}
	}

	void makeTetrahedron (float size, Vector3 basePoint) {
		print("makeTetrahedron");

		Vector3 p0 = basePoint;
		Vector3 p1 = basePoint + new Vector3(size,0,0);
		Vector3 p2 = basePoint + new Vector3(size*0.5f,0,Mathf.Sqrt(0.75f)*size);
		Vector3 p3 = basePoint + new Vector3(size*0.5f,Mathf.Sqrt(0.75f)*size,Mathf.Sqrt(0.75f)/3*size);

		// TODO: Need to add vertices to the existing set 
		// (not replace the existing vertices with a new set)
		mesh.vertices = new Vector3[]{p0,p1,p2,p3};
		mesh.triangles = new int[]{
			0,1,2,
			0,2,3,
			2,1,3,
			0,3,1
		};
			
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();


	}





}




