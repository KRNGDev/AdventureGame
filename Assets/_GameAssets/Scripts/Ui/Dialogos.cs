using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogos : MonoBehaviour
{
    public String nombreNPC;
    [TextArea] public String dialogo;
    public bool tieneCondicion;
    private bool condicion;
    public bool hablado;
    public string nombreEvento = "Porton";
    [TextArea] public String dialogo2;
    Transform panel;
    public GameObject panelDialogos;
    public GameObject objetoSpaw;
    public GameObject porton;
    public Transform puntoSpawn;

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
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
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
                condicion = gm.condicion;
                if (!condicion)
                {
                    GameObject dialogos = Instantiate(panelDialogos, panel);
                    dialogos.GetComponentsInChildren<TextMeshProUGUI>()[1].text = nombreNPC;
                    dialogos.GetComponentsInChildren<TextMeshProUGUI>()[0].text = dialogo;
                    hablado = true;
                    if (nombreEvento == "espada" && gm.puntosActuales > 0)
                    {
                        condicion = true;
                        gm.QuitarPuntos();
                        SpawnObjeto();

                    }

                }
                else
                {
                    GameObject dialogos = Instantiate(panelDialogos, panel);
                    dialogos.GetComponentsInChildren<TextMeshProUGUI>()[1].text = nombreNPC;
                    dialogos.GetComponentsInChildren<TextMeshProUGUI>()[0].text = dialogo2;
                    TipoEvento(nombreEvento);
                    hablado = true;


                }
            }
        }
    }
    public void TipoEvento(string nombre)
    {
        porton.GetComponent<Animator>().SetBool("Abrir", true);
    }
    public void SpawnObjeto()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().condicion = true;


        Instantiate(objetoSpaw, puntoSpawn);

    }

}

