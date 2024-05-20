using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.VisualScripting;
using UnityEngine;

public class Evento : MonoBehaviour
{

    public string objetivoMirar = "Mirar";
    public float tiempoEspera = 2;
    public bool dialogoAntes = true;
    public bool dialogoDespues = false;
    public string conversacion;
    private Dialogos dialogo;
    // Start is called before the first frame update
    void Start()
    {


    }

    private void OnTriggerEnter(Collider other)
    {
        dialogo = other.GetComponent<Dialogos>();
        if (other.CompareTag("Player"))
        {
            MirarEvento();

        }
    }

    private void MirarEvento()
    {
        GameObject mainCamara = GameObject.FindWithTag("MainCamera");
        mainCamara.GetComponent<TopDownCamara>().m_SmoothSpeed = 10;
        mainCamara.GetComponent<TopDownCamara>().m_Distancia = 5;
        mainCamara.GetComponent<TopDownCamara>().m_Altura = 10;

        mainCamara.GetComponent<TopDownCamara>().BuscarTarget(objetivoMirar);
        StartCoroutine(CambiarDespuesDe(tiempoEspera));
        if (dialogoAntes)
        {

            Dialogo();
        }
    }

    IEnumerator CambiarDespuesDe(float tiempo)
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(tiempo);

        GameObject mainCamara = GameObject.FindWithTag("MainCamera");
        mainCamara.GetComponent<TopDownCamara>().m_SmoothSpeed = 10;
        dialogo.hablado = false;

        mainCamara.GetComponent<TopDownCamara>().BuscarTarget("Player");
        mainCamara.GetComponent<TopDownCamara>().m_SmoothSpeed = 1;
        mainCamara.GetComponent<TopDownCamara>().m_Distancia = 7.57f;
        mainCamara.GetComponent<TopDownCamara>().m_Altura = 12.6f;
        Destroy(GameObject.Find(objetivoMirar));
        if (dialogoDespues)
        {

            Dialogo();
        }
        Destroy(gameObject);
    }
    public void Dialogo()
    {


        dialogo.nombreNPC = PlayerPrefs.GetString("Nombre");
        dialogo.dialogo = conversacion;
        dialogo.Dialogo();

    }
}
