using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour {
    public Texture texture;
    internal Mesh localMesh;
	// Use this for initialization
	void Start () {
        MeshSetup();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void MeshSetup()
    {
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();


        localMesh = new Mesh();

        localMesh.vertices = HexMetrics.getVertices();
        createMoreVerts(4);
        localMesh.triangles = HexMetrics.getTriangles();
       // localMesh.uv = HexMetrics.getUV();

        localMesh.RecalculateNormals();

        meshFilter.mesh = localMesh;

        
        GetComponent<Renderer>().material.mainTexture = texture;
    }

    public Mesh getMesh()
    {
        return localMesh;
    }
    //<summary>
    //Creates more verticies 
    //</summary>
    private void createMoreVerts(int multipiler) {
        List<Vector3> verticies = new List<Vector3>(localMesh.vertices);
        multipiler = multipiler + 1;
        for (int i = 1; i < multipiler -1; i++) {
            float divider = i / multipiler;
            verticies.AddRange(HexMetrics.getVerticiesScaled(divider));
        }
        localMesh.vertices = verticies.ToArray();
    }
}
