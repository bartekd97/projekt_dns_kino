using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailScript : MonoBehaviour {
    public enum SailType
    {
        Triangle,
        Square
    }

    public GameObject SailObject;
    public GameObject SailCollapsedObject;
    public float level = 1f;
    public float maxLevel = 1f;
    public SailType sailType;

    public Transform vertex;

    private const float _CollapseLevel = 0.05f;

    private float _startTime;
    private float timeSinceStart;

    private Material mainMaterial;
    private Material collapsedMaterial;
    private Texture2D damageMask;
    private float damageLevel = 0f;

    private Vector3[] _originalVertices;
	void Start () {
        _startTime = Time.time;
        MeshFilter mf = SailObject.GetComponent<MeshFilter>();
        _originalVertices = mf.mesh.vertices;

        damageMask = new Texture2D(128, 128, TextureFormat.RGB24, false);
        Color[] clrs = new Color[128 * 128];
        for (int i = 0; i < 128 * 128; i++)
            clrs[i] = Color.red;
        damageMask.SetPixels(0, 0, 128, 128, clrs);
        damageMask.Apply();
        if (mainMaterial)
            mainMaterial.SetTexture("_Mask", damageMask);

        //SetDamageLevel(1f);
    }
	
	private float _prevLevel = 0f;
	void Update () {
        timeSinceStart = Time.time - _startTime;

        if (_prevLevel != level)
        {
            _prevLevel = level;
            MeshFilter mf = SailObject.GetComponent<MeshFilter>();
            Vector3[] vertices = mf.mesh.vertices;
            //float wv, wvmul;
            if (sailType == SailType.Triangle)
            {
                for (int i = 0; i < mf.mesh.vertexCount; i++)
                {
                    //wv = Mathf.Sin((timeSinceStart * 2f + vertices[i].x) / 1f) * 0.5f + 1f;
                    //wvmul = 1f - Mathf.Abs((vertices[i].y + 0.5f) * 2f);
                    //wvmul += 1f - Mathf.Abs((vertices[i].x + 0.5f) * 2f);
                    //vertices[i].z = wv * wvmul * 0.5f * level;
                    vertices[i].y = _originalVertices[i].y - (_originalVertices[i].y - _originalVertices[i].x) * (1f - (_prevLevel * maxLevel));
                }
            }
            mf.mesh.vertices = vertices;
            mf.mesh.RecalculateNormals();
            Vector3 vertPos = vertex.localPosition;
            vertPos.y = -1f * _prevLevel * maxLevel;
            vertex.localPosition = vertPos;

            if (_prevLevel > _CollapseLevel)
            {
                SailObject.SetActive(true);
                SailCollapsedObject.SetActive(false);
            }
            else
            {
                SailObject.SetActive(false);
                SailCollapsedObject.SetActive(true);
            }
        }
    }

    public void SetSkin(SailSkin skin)
    {
        if (sailType == SailType.Triangle)
        {
            mainMaterial = new Material(skin.mainTriangle);
            collapsedMaterial = new Material(skin.collapsedTriangle);
        }
        else if (sailType == SailType.Square)
        {
            mainMaterial = new Material(skin.mainSquare);
            collapsedMaterial = new Material(skin.collapsedSquare);
        }
        if (damageMask)
            mainMaterial.SetTexture("_Mask", damageMask);
        SailObject.GetComponent<MeshRenderer>().material = mainMaterial;
        SailCollapsedObject.GetComponent<MeshRenderer>().material = collapsedMaterial;
    }

    public void SetDamageLevel(float lvl)
    {
        if (damageLevel == lvl)
            return;

        int maxHoles = 0;
        switch (sailType)
        {
            case SailType.Square:
                maxHoles = 200;
                break;
            case SailType.Triangle:
                maxHoles = 100;
                break;
            default:
                break;
        }

        Color[] cHole = new Color[16];
        for (int i = 0; i < 16; i++)
            cHole[i] = Color.black;
        cHole[0] = Color.red;
        cHole[3] = Color.red;
        cHole[12] = Color.red;
        cHole[15] = Color.red;

        int currHoles = (int)(maxHoles * damageLevel);
        int toHoles = (int)(maxHoles * lvl);
        Color[] cCurr;
        for (int i=0; i<(toHoles-currHoles); i++)
        {
            int x = Random.Range(0, 124);
            int y = 124;
            if (sailType == SailType.Square)
                y = Random.Range(0, 124);
            else if (sailType == SailType.Triangle)
                y = 124-Random.Range(124-x, 124);
            cCurr = damageMask.GetPixels(x, y, 4, 4);
            for (int j=0; j < 16; j++)
                if (cCurr[j] == Color.red && cHole[j] == Color.black)
                    cCurr[j] = Color.black;
            damageMask.SetPixels(x, y, 4, 4, cCurr);
        }
        damageMask.Apply();

        damageLevel = lvl;
    }

    public void SetSailOffset( float off )
    {
        mainMaterial.SetFloat("_Offset", off);
    }
    public void SetSailLevel(float lvl)
    {
        level = lvl;
    }
}
