using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererEx : MonoBehaviour {
    public Transform[] points;
    public Material mat;
    public float width = 0.1f;

    private LineRenderer lren;
	void Start () {
        lren = gameObject.AddComponent<LineRenderer>();
        lren.material = mat;
        lren.widthMultiplier = width;
        lren.positionCount = points.Length;
	}

	void Update () {
        Vector3[] vecs = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
            vecs[i] = points[i].position;
        lren.SetPositions(vecs);
	}
}
