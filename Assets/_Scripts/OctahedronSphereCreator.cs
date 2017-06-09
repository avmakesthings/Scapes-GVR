using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OctahedronSphereCreator {

	public static Mesh Create (int subdivisions, float radius) {
        Vector3[] vertices = {
            Vector3.down, Vector3.down, Vector3.down, Vector3.down,
            Vector3.forward,
            Vector3.left,
            Vector3.back,
            Vector3.right,
            Vector3.up, Vector3.up, Vector3.up, Vector3.up
        };

        int[] triangles = {
            0, 4, 5,
            1, 5, 6,
            2, 6, 7,
            3, 7, 4,

             8, 5, 4,
             9, 6, 5,
            10, 7, 6,
            11, 4, 7
        };


        Vector3[] normals = new Vector3[vertices.Length];
        Normalize(vertices, normals);

        Vector2[] uv = new Vector2[vertices.Length];
        CreateUV(vertices, uv);

        if (radius != 1f) {
            for (int i = 0; i < vertices.Length; i++) {
                vertices[i] *= radius;
            }
        }

        Mesh mesh = new Mesh();
        mesh.name = "Octahedron Sphere";
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uv;
        mesh.triangles = triangles;
        return mesh;
	}

	//normalize vertices
	private static void Normalize (Vector3[] vertices, Vector3[] normals) {
        for (int i = 0; i < vertices.Length; i++) {
            normals[i] = vertices[i] = vertices[i].normalized;
        }
    }

	//convert vertex positions into texture coordinates
	private static void CreateUV (Vector3[] vertices, Vector2[] uv) {
        for (int i = 0; i < vertices.Length; i++) {
            Vector3 v = vertices[i];
            Vector2 textureCoordinates;
			//vertical coordinate
            textureCoordinates.x = Mathf.Atan2(v.x, v.z) / (-2f * Mathf.PI);
            if (textureCoordinates.x < 0f) {
                textureCoordinates.x += 1f;
            }
			//horizontal coordinate
            textureCoordinates.y = Mathf.Asin(v.y) / Mathf.PI + 0.5f;
            uv[i] = textureCoordinates;
        }
		uv[vertices.Length - 4].x = uv[0].x = 0.125f;
        uv[vertices.Length - 3].x = uv[1].x = 0.375f;
        uv[vertices.Length - 2].x = uv[2].x = 0.625f;
        uv[vertices.Length - 1].x = uv[3].x = 0.875f;
    }


}
