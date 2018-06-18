using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShipControler : NetworkBehaviour {

    public Ship ship;

    private Rigidbody rb;

    public float rotationForce = 0f;
    [SyncVar]
    public float toRotationForce = 0f;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        float dt = Time.fixedDeltaTime;
        float dtm = dt * 20f;

        float dmgmult = (ship.CurrentHealth / ship.MaximumHealth) * 0.7f + 0.3f;
        if (!ship.isDrowing && !WaitingScreen.screen.gameObject.activeSelf)
            rb.AddRelativeForce(Vector3.forward * dtm * 50f * ship.ForwardSpeed * ship.sailLevel * dmgmult);

        float cToRotationForce = 0f;

        
        if (isLocalPlayer && !ship.isDrowing && !WaitingScreen.screen.gameObject.activeSelf)
        {
            if (Input.GetKey(KeyCode.W))
            {
                ship.ExpandSails();
            }
            if (Input.GetKey(KeyCode.S))
            {
                ship.CollapseSails();
            }

            if (Input.GetKey(KeyCode.A))
            {
                cToRotationForce = -ship.TurnForce;
                ship.TurnLeftSails();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                cToRotationForce = ship.TurnForce;
                ship.TurnRightSails();
            }
            else
            {
                cToRotationForce = 0f;
                ship.TurnForwardSails();
            }

            if (toRotationForce != cToRotationForce)
            {
                if (isServer)
                    toRotationForce = cToRotationForce;
                else
                    CmdToRotationForce(cToRotationForce);
            }
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

    [Command]
    public void CmdToRotationForce(float value)
    {
        toRotationForce = value;
    }
}
