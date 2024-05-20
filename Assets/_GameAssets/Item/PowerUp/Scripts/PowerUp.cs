using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public GameObject pickupEffect;
    public GameManager gm;

    public void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Espada"))
        {
            Pickup();
        }
    }

    public void Pickup()
    {
        Item item = GetComponent<Item>();
        if (item != null)
        {
            if (item.objeto == true)
            {
                gm.inventarioItem.Add(item);
            }
            if (item.equipo == true)
            {
                gm.inventarioEquipacion.Add(item);
            }
        }


        Instantiate(pickupEffect, transform.position, transform.rotation);


        Destroy(gameObject);
    }
}
