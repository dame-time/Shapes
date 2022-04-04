using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Plane : MonoBehaviour
{
    [Range(0, 10)][SerializeField] private int size = 1;
    [Range(0, 7)][SerializeField] private int resolution = 0;

    private MeshFilter filter;
    private MeshRenderer meshRenderer;

    private int currentResolution;
    private int currentSize;

    private void Start()
    {
        GenerateInitalMesh();
    }

    private void OnValidate()
    {
        if (!filter || !meshRenderer)
            GenerateInitalMesh();

        if (resolution != currentResolution || currentSize != size)
            ShapeGenerator.UpdatePlaneMesh(filter, resolution, size);

        currentResolution = resolution;
        currentSize = size;
    }

    private void GenerateInitalMesh()
    {
        this.name = "Plane";

        filter = this.GetComponent<MeshFilter>();
        meshRenderer = this.GetComponent<MeshRenderer>();

        currentResolution = resolution;
        currentSize = size;

        ShapeGenerator.GeneratePlaneMesh(meshRenderer, filter, resolution, size);
    }
}
