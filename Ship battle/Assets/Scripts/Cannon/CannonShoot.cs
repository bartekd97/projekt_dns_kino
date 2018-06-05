using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShoot : MonoBehaviour {

    public GameObject cannonBall;
    public float firePower;

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootCannon();
        }
	}

    public void ShootCannon()
    {
        GameObject thisCannonBall = Instantiate(cannonBall, transform.position, transform.rotation);
        thisCannonBall.GetComponent<Rigidbody>().AddRelativeForce(0, 0, firePower, ForceMode.Impulse);
    }
}
