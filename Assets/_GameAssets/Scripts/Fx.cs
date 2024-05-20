using System;
using System.Collections.Generic;
using UnityEngine;

public class Fx : MonoBehaviour
{
    [Header("Efectos Genericos")]
    public GameObject fuego;
    public GameObject hielo;
    public GameObject electric;






    public void FxEnemigo(String particula)
    {

        switch (particula)
        {

            case "fuego":
                GameObject ardiendo = Instantiate(fuego, transform.position, transform.rotation);
                ardiendo.transform.parent = transform;
                break;
            case "hielo":
                GameObject congelado = Instantiate(hielo, transform.position, transform.rotation);
                congelado.transform.parent = transform;
                break;
            case "electric":
                GameObject electrificado = Instantiate(electric, transform.position, transform.rotation);
                electrificado.transform.parent = transform;
                break;

        }

    }
    public void FxPlayer(String particula)
    {

        switch (particula)
        {

            case "fuego":
                GameObject ardiendo = Instantiate(fuego, transform.position, transform.rotation);
                ardiendo.transform.parent = transform;
                break;
            case "hielo":
                GameObject congelado = Instantiate(hielo, transform.position, transform.rotation);
                congelado.transform.parent = transform;
                break;
            case "electric":
                GameObject electrificado = Instantiate(electric, transform.position, transform.rotation);
                electrificado.transform.parent = transform;
                break;

        }

    }

}
