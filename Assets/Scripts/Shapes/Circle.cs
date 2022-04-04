using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Circle : MonoBehaviour
{
    [Range(0, 10)] [SerializeField] private int size = 1;
    [Range(1, 7)] [SerializeField] private int resolution = 1;

    private MeshFilter filter;
    private MeshRenderer meshRenderer;

    private int currentResolution;
    private int currentSize;

    private void Start()
    {
        GenerateInitalMesh();
        GenerateCircle();
    }

    private void GenerateCircle()
    {
        Mesh newMesh = ShapeGenerator.GenerateCircleFromMesh(filter.sharedMesh);

        filter.sharedMesh.vertices = newMesh.vertices;
        filter.sharedMesh.normals = newMesh.normals;

        filter.sharedMesh.RecalculateNormals();
    }

    private void OnValidate()
    {
        if (!filter || !meshRenderer)
        {
            GenerateInitalMesh();
            GenerateCircle();
        }

        if (resolution != currentResolution || currentSize != size)
        {
            ShapeGenerator.UpdatePlaneMesh(filter, resolution, size);
            GenerateCircle();
        }

        currentResolution = resolution;
        currentSize = size;
    }

    private void GenerateInitalMesh()
    {
        this.name = "Circle";

        filter = this.GetComponent<MeshFilter>();
        meshRenderer = this.GetComponent<MeshRenderer>();

        currentResolution = resolution;
        currentSize = size;

        ShapeGenerator.GeneratePlaneMesh(meshRenderer, filter, resolution, size);
    }
}