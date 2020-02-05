using UnityEngine;
using System.Collections.Generic;

public class WaterWave : MonoBehaviour
{
    public float size = 1;
    public int gridSize = 16;
    Vector3 waveSource1 = new Vector3(2.0f, 0.0f, 2.0f);
    public float waveFrequency = 0.53f;
    public float waveHeight = 0.48f;
    public float waveLength = 0.71f;
    public bool edgeBlend = true;
    public bool forceFlatShading = true;
    public Material my_material;
    Mesh mesh;
    Vector3[] verts;
    private MeshFilter filter;

    void Start()
    {
        Camera.main.depthTextureMode |= DepthTextureMode.Depth;
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        GetComponent<MeshRenderer>().material = my_material;
        GetComponent<MeshFilter>().mesh = GenerateMesh();
        MeshFilter mf = GetComponent<MeshFilter>();
        makeMeshLowPoly(mf);

    }
    private Mesh GenerateMesh() 
    {
        Mesh mesh;
        mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();

        for (int x = 0; x < gridSize + 1; x++) 
        {
            for (int y = 0; y < gridSize + 1; y++) 
            {
                vertices.Add(new Vector3(-size * 0.5f + size*(x/((float)gridSize)),0,-size * 0.5f + size* (y/((float)gridSize))));
                normals.Add(Vector3.up);
                uvs.Add(new Vector2(x / (float)gridSize, y / (float)gridSize));
            }
        }

        List<int> triangles = new List<int>();
        int vertCount = gridSize + 1;
        for (int i = 0; i < vertCount * vertCount - vertCount; i++) 
        {
            if ((i + 1) % vertCount == 0) 
            {
                continue;
            }
            triangles.AddRange(new List<int>() { i + 1 + vertCount, i + vertCount, i, i, i + 1, i + vertCount + 1 });
        }

        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(triangles, 0);


        return mesh;
    }

    MeshFilter makeMeshLowPoly(MeshFilter mf)
    {
        mesh = mf.sharedMesh;//Change to sharedmesh? 
        Vector3[] oldVerts = mesh.vertices;
        int[] triangles = mesh.triangles;
        Vector3[] vertices = new Vector3[triangles.Length];
        for (int i = 0; i < triangles.Length; i++)
        {
            vertices[i] = oldVerts[triangles[i]];
            triangles[i] = i;
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        verts = mesh.vertices;
        return mf;
    }

    void setEdgeBlend()
    {
        if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
        {
            edgeBlend = false;
        }
        if (edgeBlend)
        {
            Shader.EnableKeyword("WATER_EDGEBLEND_ON");
            if (Camera.main)
            {
                Camera.main.depthTextureMode |= DepthTextureMode.Depth;
            }
        }
        else
        {
            Shader.DisableKeyword("WATER_EDGEBLEND_ON");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalcWave();
        setEdgeBlend();
    }

    void CalcWave()
    {
        for (int i = 0; i < verts.Length; i++)
        {
            Vector3 v = verts[i];
            v.y = 0.0f;
            float dist = Vector3.Distance(v, waveSource1);
            dist = (dist % waveLength) / waveLength;
            v.y = waveHeight * Mathf.Sin(Time.time * Mathf.PI * 2.0f * waveFrequency
            + (Mathf.PI * 2.0f * dist));
            verts[i] = v;
        }
        mesh.vertices = verts;
        mesh.RecalculateNormals();
        mesh.MarkDynamic();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}