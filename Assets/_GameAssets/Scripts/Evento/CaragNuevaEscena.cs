using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaragNuevaEscena : MonoBehaviour
{
    public string escena;
    public string UbicacionSalida;
    private void OnTriggerEnter(Collider other)
    {
        GameObject gm = GameObject.Find("GameManager");
        GameObject escene = GameObject.Find("EsceneManager");

        if (other.CompareTag("Player"))
        {
            //Guarda el nombre de la ubicacion de donde voy a salir para el GameManager
            gm.GetComponent<GameManager>().nombreUbicacion = UbicacionSalida;


            escene.GetComponent<EsceneManager>().CargarEscena(escena);


        }

    }
}
