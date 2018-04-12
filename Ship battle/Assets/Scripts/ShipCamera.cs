﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCamera : MonoBehaviour {

    public Transform lookAt;
    public Transform camPosition;
    public Transform camParent;
    public Ocean ocean;

    public float posY = 10f;
    public float distance = 70f;

	void Start () {
		
	}
	
	void Update () {
        camParent.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);

        posY -= Input.GetAxis("Mouse Y");
        if (posY > 30f)
            posY = 30f;
        else if (posY < 5f)
            posY = 5f;

        distance = distance - Input.GetAxis("Mouse ScrollWheel") * 20f;
        if (distance < 20f)
            distance = 20f;
        if (distance > 110f)
            distance = 110f;

        Vector3 toPosition = camParent.position + (camPosition.position - camParent.position).normalized*distance;
        toPosition.y = Mathf.Max(posY, ocean.GetWaterLevel(toPosition) + 1f);
        transform.position = transform.position + (toPosition - transform.position) * 0.1f;
        transform.LookAt( lookAt );
	}
}