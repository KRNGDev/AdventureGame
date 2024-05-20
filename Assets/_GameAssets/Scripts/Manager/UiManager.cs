using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject controller;
    public GameObject panelPersonaje;
    public GameObject btnMenu;
    public GameObject panelItems;
    public GameObject panelDescripcion;
    public GameObject panelEquipo;
    public GameObject panelOpciones;
    public GameObject panelBtnOpciones;
    public GameObject panelNoUsarMas;
    public GameObject panelYaEstaEquipado;

    [Header("Sonido UI")]
    public GameObject prefabSonido;
    public AudioClip sonidoAtras;
    public AudioClip sonidoSelect;
    public AudioClip sonidoconfrmar;
    public AudioClip sonidoMenu;
    public AudioClip sonidoEquipar;
    public AudioClip sonidoQuitar;
    public AudioClip sonidoUsar;
    public AudioClip sonidoMensaje;
    public AudioClip sonidoBack;
    private GameObject main;
    public void AbrirPanel(string panel)
    {
        switch (panel)
        {

            case "menu":
                panelMenu.SetActive(true);
                controller.SetActive(false);
                btnMenu.SetActive(false);
                GameObject menu = Instantiate(prefabSonido, transform.position, transform.rotation);
                menu.GetComponent<AudioSource>().clip = sonidoMenu;
                menu.GetComponent<AudioSource>().Play();
                Time.timeScale = 0;
                break;
            case "objetos":
                panelItems.SetActive(true);
                panelDescripcion.SetActive(true);
                GameObject obje = Instantiate(prefabSonido, transform.position, transform.rotation);
                obje.GetComponent<AudioSource>().clip = sonidoSelect;
                obje.GetComponent<AudioSource>().Play();
                GetComponent<GameManager>().MostrarPrimerObjeto();

                break;
            case "equipo":
                panelEquipo.SetActive(true);
                panelDescripcion.SetActive(true);
                GameObject equi = Instantiate(prefabSonido, transform.position, transform.rotation);
                equi.GetComponent<AudioSource>().clip = sonidoSelect;
                equi.GetComponent<AudioSource>().Play();
                GetComponent<GameManager>().MostrarPrimerEquipo();

                break;
            case "opciones":
                panelOpciones.SetActive(true);
                panelBtnOpciones.SetActive(true);
                panelDescripcion.SetActive(false);
                GameObject opci = Instantiate(prefabSonido, transform.position, transform.rotation);
                opci.GetComponent<AudioSource>().clip = sonidoSelect;
                opci.GetComponent<AudioSource>().Play();

                break;

        }
    }
    public void CerrarPanel(string panel)
    {
        switch (panel)
        {

            case "menu":
                panelMenu.SetActive(false);
                controller.SetActive(true);
                btnMenu.SetActive(true);
                GameObject cerrarMenu = Instantiate(prefabSonido, transform.position, transform.rotation);
                cerrarMenu.GetComponent<AudioSource>().clip = sonidoAtras;
                cerrarMenu.GetComponent<AudioSource>().Play();
                Time.timeScale = 1;
                break;
            case "objetos":
                panelItems.SetActive(false);
                break;
            case "equipo":
                panelEquipo.SetActive(false);

                break;
            case "opciones":
                panelOpciones.SetActive(false);
                panelBtnOpciones.SetActive(false);
                break;
            case "noUsarMas":
                panelNoUsarMas.SetActive(false);

                break;
            case "yaEstaEquipado":
                panelYaEstaEquipado.SetActive(false);

                break;
           
        }
    }

    public void BuscarMain()
    {
        main = GameObject.Find("MainManager");
        main.GetComponent<MainManager>().SaveAudioVolume();
    }
    public void LoadScene()
    {

    }

}
