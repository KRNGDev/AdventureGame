using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public GameObject explosionPrefab;
    public AudioClip explosionSound;
    void OnTriggerEnter(Collider other)
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, this.transform.rotation);
        GetComponent<AudioSource>().PlayOneShot(explosionSound);
        //Destroy(gameObject);
    }
}
