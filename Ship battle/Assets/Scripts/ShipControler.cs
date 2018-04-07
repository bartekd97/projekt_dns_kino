using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControler : MonoBehaviour {

    private Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
        if (Input.GetKey(KeyCode.W)){ 
        
            rb.AddForce(Vector3.right * 1000);
            Debug.Log("W");
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.right * -1000);
            Debug.Log("S");
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(Vector3.down * Time.deltaTime * 20000);
            Debug.Log("A");
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(Vector3.up * Time.deltaTime * 20000);
            Debug.Log("D");
        }
    }
}
