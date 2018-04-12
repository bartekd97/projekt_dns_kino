using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour {

    public float partSize = 10f;
    public int partSegments = 100;

    public Material WaterMaterial;

    public float waveScale = 1.0f;
    public float waveSpeed = 1.0f;
    public float waveDistance = 1f;
    public float waveNoiseStrength = 1f;
    public float waveNoiseWalk = 1f;

    private float _startTime;
    private float timeSinceStart;


    private List<GameObject> oceanParts = new List<GameObject>();


	// Use this for initialization
	void Start () {
        CreateOceanPart();

        _startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        timeSinceStart = Time.time - _startTime;
        foreach( GameObject go in oceanParts)
        {
            MeshFilter mf = go.GetComponent<MeshFilter>();
            Vector3[] vertices = mf.mesh.vertices;
            for (int i = 0; i < mf.mesh.vertexCount; i++)
            {
                vertices[i].y = GetWaterLevel(vertices[i]);
            }
            mf.mesh.vertices = vertices;
            mf.mesh.RecalculateNormals();
        }
    }

    public float GetWaterLevel(Vector3 position)
    {
        float mscale = (partSize / partSegments);
        return OceanWave.SinXWave(position, waveSpeed * mscale, waveScale, waveDistance * mscale, waveNoiseStrength, waveNoiseWalk, timeSinceStart);
    }

    private void CreateOceanPart()
    {
        GameObject go = new GameObject("oceanpart");
        go.transform.parent = transform;

        go.AddComponent<MeshFilter>();
        go.AddComponent<MeshRenderer>();

        MeshFilter mf = go.GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        int vecnum = (partSegments + 1) * (partSegments + 1);
        Vector3[] vertices = new Vector3[vecnum];
        Vector3[] normals = new Vector3[vecnum];
        Vector2[] uvs = new Vector2[vecnum];
        float pX, pZ, uvX, uvY;
        float pStep = partSize / partSegments;
        float uvStep = 1f / partSegments;
        pX = -partSize / 2;
        uvX = 0f;
        int ind;
        for (int i = 0; i <= partSegments; i++)
        {
            pZ = -partSize / 2;
            uvY = 0f;
            for (int j = 0; j <= partSegments; j++)
            {
                ind = i * (partSegments + 1) + j;
                vertices[ind] = new Vector3(pX, 0, pZ);
                normals[ind] = Vector3.up;
                uvs[ind] = new Vector2(uvX, uvY);
                pZ += pStep;
                uvY += uvStep;
            }
            pX += pStep;
            uvX += uvStep;
        }
        int[] triangles = new int[partSegments * partSegments * 2 * 3];
        int tn = 0;
        int v1, v2, v3, v4;
        for (int i = 0; i < partSegments; i++)
        {
            for (int j = 0; j < partSegments; j++)
            {
                v1 = i * (partSegments + 1) + j;
                v2 = (i+1) * (partSegments + 1) + j;
                v3 = v1 + 1;
                v4 = v2 + 1;

                triangles[tn] = v3;
                triangles[tn+1] = v2;
                triangles[tn+2] = v1;

                triangles[tn + 3] = v2;
                triangles[tn + 4] = v3;
                triangles[tn + 5] = v4;

                tn += 6;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.normals = normals;
        mesh.triangles = triangles;

        ////////////////////////////////////////////////
        MeshRenderer mr = go.GetComponent<MeshRenderer>();
        mr.material = WaterMaterial;


        oceanParts.Add(go);
    }
}
