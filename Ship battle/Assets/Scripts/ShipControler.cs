using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControler : MonoBehaviour {

    public Ship ship;

    private Rigidbody rb;

    public float rotationForce = 0f;
    private float toRotationForce = 0f;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        float dt = Time.fixedDeltaTime;
        float dtm = dt * 20f;

        rb.AddRelativeForce(Vector3.forward * dtm * 50f * ship.ForwardSpeed * ship.sailLevel);
        if (Input.GetKey(KeyCode.W)){

            ship.ExpandSails();
        }
        if (Input.GetKey(KeyCode.S))
        {
            ship.CollapseSails();
        }

        if (Input.GetKey(KeyCode.A))
        {
            toRotationForce = -ship.TurnForce;
            ship.TurnLeftSails();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            toRotationForce = ship.TurnForce;
            ship.TurnRightSails();
        }
        else
        {
            toRotationForce = 0f;
            ship.TurnForwardSails();
        }
        if (rotationForce != toRotationForce * ship.sailLevel)
        {
            float diff = toRotationForce * ship.sailLevel - rotationForce;
            float add = dt * 5f;
            if (Mathf.Abs(diff) <= add)
                rotationForce = toRotationForce * ship.sailLevel;
            else
                rotationForce = rotationForce + add * Mathf.Sign(diff);
        }
        transform.Rotate(0f, rotationForce * dt * 0.2f, 0f, Space.Self);
    }
}
