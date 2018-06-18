using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSkinSetup : MonoBehaviour {
    public MeshRenderer[] meshes;
    public void ApplySkin(ShipSkin skin)
    {
        for (int i = 0; i < meshes.Length; i++)
            meshes[i].material = skin.materials[i];
    }
}
