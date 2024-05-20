using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosManager : MonoBehaviour
{
    public GameObject porton;

    public void Evento(String nombre)
    {
        switch (nombre)
        {
            case "Porton":
                porton.GetComponent<Animator>().SetBool("Abrir", true);

                break;
        }
    }
}
