using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {
    public float damage = 5.7f;
    public GameObject explosionParent;
    public ParticleSystem waterSplash;

    public AudioClip shootSFX;
    public float shootVolume = 0.3f;
    public AudioClip explosionSFX;
    public float explosionVolume = 0.7f;
    public AudioClip splashSFX;
    public float splashVolume = 0.7f;

    private AudioSource asource;
    private void Start()
    {
        asource = gameObject.AddComponent<AudioSource>();
        asource.playOnAwake = true;
        if (shootSFX)
            asource.PlayOneShot(shootSFX, shootVolume);
    }

    private bool exploded = false;
    private bool splashed = false;
    private void Update()
    {
        if (exploded || splashed)
            return;
        float oY = Ocean.LastOceanObject.GetWaterLevel(transform.position);
        if (oY >= transform.position.y)
        {
            splashed = true;
            waterSplash.Play();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            if (splashSFX)
                asource.PlayOneShot(splashSFX, splashVolume);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (exploded || splashed)
            return;
        if (other.gameObject.tag == "ShipCollision")
        {
            Ship targetShip = other.gameObject.GetComponentInParent<Ship>();
            if (GetComponentInParent<Ship>() != targetShip && !targetShip.isDrowing)
            {
                GetComponent<MeshRenderer>().enabled = false;
                targetShip.TakeDamage(damage);
                explosionParent.SetActive(true);
                exploded = true;
                if (explosionSFX)
                    asource.PlayOneShot(explosionSFX, explosionVolume);
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
