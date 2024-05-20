using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("Abierta", true);
        }
    }
}
