using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof (MeshFilter), typeof (MeshRenderer))]
public class HexMesh : MonoBehaviour {
    Mesh hexMesh;
    List<Vector3> vertices;
    List<int> triangles;
    Canvas gridCanvas;

    private void Awake() {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        hexMesh.name = "Hex Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
        gridCanvas = GetComponentInChildren<Canvas>();

    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
