using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class makeObj : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Mesh mesh = new Mesh();

        List<Vector3> verticies = new List<Vector3>();
        List<int> indicies = new List<int>();

        verticies.Add(new Vector3(0.5f, 0.5f, -0.5f));
        verticies.Add(new Vector3(-0.5f, 0.5f, 0.5f));
        verticies.Add(new Vector3(0.5f, 0.5f, 0.5f));

        verticies.Add(new Vector3(-0.5f, 0.5f, -0.5f));
        verticies.Add(new Vector3(-0.5f, 0.5f, 0.5f));
        verticies.Add(new Vector3(0.5f, 0.5f, -0.5f));

        // Front
        verticies.Add(new Vector3(0.5f, 0.5f, -0.5f));
        verticies.Add(new Vector3(0f, -0.5f, 0f));
        verticies.Add(new Vector3(-0.5f, 0.5f, -0.5f));

        // Left
        verticies.Add(new Vector3(-0.5f, 0.5f, -0.5f));
        verticies.Add(new Vector3(0f, -0.5f, 0f));
        verticies.Add(new Vector3(-0.5f, 0.5f, 0.5f));

        // Back
        verticies.Add(new Vector3(-0.5f, 0.5f, 0.5f));
        verticies.Add(new Vector3(0f, -0.5f, 0f));
        verticies.Add(new Vector3(0.5f, 0.5f, 0.5f));

        // Right
        verticies.Add(new Vector3(0.5f, 0.5f, 0.5f));
        verticies.Add(new Vector3(0f, -0.5f, 0f));
        verticies.Add(new Vector3(0.5f, 0.5f, -0.5f));



        for (int i = 0; i < verticies.Count; i++)
        {
            indicies.Add(i);
        }

        mesh.SetVertices(verticies);
        mesh.SetTriangles(indicies, 0);
        mesh.RecalculateNormals();

        transform.gameObject.AddComponent<MeshFilter>().sharedMesh = mesh;
	}
	
}
