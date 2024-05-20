using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogos : MonoBehaviour
{
    public String nombreNPC;
    public String dialogo;
    public bool tieneCondicion;
    private bool condicion;
    public bool hablado;
    public String dialogo2;
    Transform panel;
    public GameObject panelDialogos;

    void Awake()
    {

        panel = GameObject.Find("IU").transform;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Dialogo();
        }
    }

    public void Dialogo()
    {
        if (!hablado)
        {
            if (!tieneCondicion)
            {
                GameObject dialogos = Instantiate(panelDialogos, panel);
                dialogos.GetComponentsInChildren<TextMeshProUGUI>()[1].text = nombreNPC;
                dialogos.GetComponentsInChildren<TextMeshProUGUI>()[0].text = dialogo;
                hablado = true;
            }
            else
            {
                condicion = GameObject.Find("GameManager").GetComponent<GameManager>().condicion;
                if (!condicion)
                {
                    GameObject dialogos = Instantiate(panelDialogos, panel);
                    dialogos.GetComponentsInChildren<TextMeshProUGUI>()[1].text = nombreNPC;
                    dialogos.GetComponentsInChildren<TextMeshProUGUI>()[0].text = dialogo;
                    hablado = true;

                }
                else
                {
                    GameObject dialogos = Instantiate(panelDialogos, panel);
                    dialogos.GetComponentsInChildren<TextMeshProUGUI>()[1].text = nombreNPC;
                    dialogos.GetComponentsInChildren<TextMeshProUGUI>()[0].text = dialogo2;
                    GameObject gm = GameObject.Find("GameManager");
                    gm.GetComponent<EventosManager>().Evento("Porton");
                    hablado = true;


                }
            }
        }
    }
}
