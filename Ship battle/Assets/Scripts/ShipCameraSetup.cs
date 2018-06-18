using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShipCameraSetup : NetworkBehaviour {

    public Transform lookAt;
    public Transform camParent;
    public Transform camPosition;

	void Start () {
        if (isLocalPlayer)
        {
            ShipCamera cam = Camera.main.GetComponent<ShipCamera>();
            cam.lookAt = lookAt;
            cam.camParent = camParent;
            cam.camPosition = camPosition;
            cam.ocean = Ocean.LastOceanObject;
        }
    }
	
}
