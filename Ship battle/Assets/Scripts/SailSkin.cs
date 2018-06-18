using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailSkin : MonoBehaviour {
    public static Dictionary<string, SailSkin> RegisteredSkins = new Dictionary<string, SailSkin>();

    public string name;
    public Material mainTriangle;
    public Material collapsedTriangle;
    public Material mainSquare;
    public Material collapsedSquare;

    private void Start()
    {
        RegisteredSkins.Add(name, this);
    }
    public static SailSkin GetRandomSkin()
    {
        return RegisteredSkins[(new List<string>(RegisteredSkins.Keys))[Random.Range(0, RegisteredSkins.Count - 1)]];
    }
}
