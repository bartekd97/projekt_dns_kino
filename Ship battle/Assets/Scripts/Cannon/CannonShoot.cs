using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShoot : MonoBehaviour {

    public GameObject cannonBall;
    public float firePower = 100f;

    /*
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootCannon();
        }
	}
    */

    private float latentPower;
    public void ShootCannonLatent(float power)
    {
        latentPower = power;
        StartCoroutine("LatentShoot", Random.Range(0f, 1f));
    }
    public void ShootCannonLatent()
    {
        ShootCannonLatent(firePower);
    }
    public void ShootCannon(float power)
    {
        GameObject thisCannonBall = Instantiate(cannonBall, transform.position, transform.rotation, transform);
        thisCannonBall.GetComponent<Rigidbody>().AddRelativeForce(0, 0, power, ForceMode.Impulse);
    }
    public void ShootCannon()
    {
        ShootCannon(firePower);
    }

    IEnumerator LatentShoot(float latency)
    {
        yield return new WaitForSeconds(latency);
        ShootCannon(latentPower);
    }
}
