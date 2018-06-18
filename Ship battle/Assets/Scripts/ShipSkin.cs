using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSkin : MonoBehaviour {
    public static Dictionary<string, ShipSkin> RegisteredSkins = new Dictionary<string, ShipSkin>();
    public string name;
    public Material[] materials;

    private void Start()
    {
        RegisteredSkins.Add(name, this);
    }
    public static ShipSkin GetRandomSkin()
    {
        return RegisteredSkins[(new List<string>(RegisteredSkins.Keys))[Random.Range(0, RegisteredSkins.Count - 1)]];
    }
}
