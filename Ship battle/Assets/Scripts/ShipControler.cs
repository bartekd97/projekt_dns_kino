using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControler : MonoBehaviour {

    public float TurnForce = 10f;

    private Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.W)){ 
        
            rb.AddRelativeForce(Vector3.forward * 1000);
            Debug.Log("W");
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(Vector3.forward * -1000);
            Debug.Log("S");
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeTorque(Vector3.left * Time.deltaTime * 20000 * TurnForce);
            Debug.Log("A");
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeTorque(Vector3.right * Time.deltaTime * 20000 * TurnForce);
            Debug.Log("D");
        }
    }
}
