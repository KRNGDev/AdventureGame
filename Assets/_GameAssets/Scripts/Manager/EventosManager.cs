using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosManager : MonoBehaviour
{
    public GameObject porton;
    public bool puerta;
    public bool espada;


    public void Evento(String nombre)
    {
        switch (nombre)
        {
            case "Porton":
                porton = GameObject.Find("Cylinder.003");
                porton.GetComponent<Animator>().SetBool("Abrir", true);


                break;
            case "espada":
                GameObject.Find("Espada_Corta_A").SetActive(true);

                break;
        }
    }

}
