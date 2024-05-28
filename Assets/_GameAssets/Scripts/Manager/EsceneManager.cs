using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EsceneManager : MonoBehaviour
{
    public GameObject btnJugar;
    public GameObject btnSalir;
    private GameObject panelInicio;
    private GameObject panelAcciones;
    public GameObject btnVolverNombre;
    public string nombreEscena;

    private void Awake()
    {
        GameObject main = GameObject.Find("MainManager");
        panelInicio = GameObject.Find("PanelInicio");
        panelAcciones = GameObject.Find("PanelAcciones");
        if (PlayerPrefs.HasKey(MainManager.KEY_NAME))
        {
            panelInicio?.SetActive(false);
            panelAcciones?.SetActive(true);

            // main.GetComponent<MainManager>()?.RestoreName();
        }
        else
        {
            panelInicio.SetActive(true);
            panelAcciones.SetActive(false);
        }
    }

    public void CargarEscena(String name)
    {
        switch (name)
        {
            case "exterior":
                nombreEscena = "Exterior";
                Time.timeScale = 1;
                SceneManager.LoadScene("Cargando");
                GameObject.Find("MainManager").GetComponent<AudioSource>().clip = GameObject.Find("SoundEscene").GetComponentInChildren<SoundEscene>()?.sonidoBack;
                GameObject.Find("MainManager").GetComponent<AudioSource>().Play();
                break;
            case "casa1":
                nombreEscena = "Casa1";
                Time.timeScale = 1;
                SceneManager.LoadScene("Cargando");
                GameObject.Find("MainManager").GetComponent<AudioSource>().clip = GameObject.Find("SoundEscene").GetComponentInChildren<SoundEscene>()?.sonidoBack;
                GameObject.Find("MainManager").GetComponent<AudioSource>().Play();
                break;
            case "intro":
                nombreEscena = "intro";
                SceneManager.LoadScene("Cargando");
                GameObject.Find("MainManager").GetComponent<AudioSource>().clip = GameObject.Find("SoundEscene").GetComponentInChildren<SoundEscene>()?.sonidoIntro;
                GameObject.Find("MainManager").GetComponent<AudioSource>().Play();
                break;
            case "taberna":
                nombreEscena = "Taberna";
                Time.timeScale = 1;
                SceneManager.LoadScene("Cargando");
                GameObject.Find("MainManager").GetComponent<AudioSource>().clip = GameObject.Find("SoundEscene").GetComponentInChildren<SoundEscene>()?.sonidoIntro;
                GameObject.Find("MainManager").GetComponent<AudioSource>().Play();
                break;
            case "dungeon":
                nombreEscena = "Dungeon";
                Time.timeScale = 1;
                SceneManager.LoadScene("Cargando");
                GameObject.Find("MainManager").GetComponent<AudioSource>().clip = GameObject.Find("SoundEscene").GetComponentInChildren<SoundEscene>()?.sonidoIntro;
                GameObject.Find("MainManager").GetComponent<AudioSource>().Play();
                break;
            case "bosque":
                nombreEscena = "Bosque";
                Time.timeScale = 1;
                SceneManager.LoadScene("Cargando");
                GameObject.Find("MainManager").GetComponent<AudioSource>().clip = GameObject.Find("SoundEscene").GetComponentInChildren<SoundEscene>()?.sonidoIntro;
                GameObject.Find("MainManager").GetComponent<AudioSource>().Play();
                break;
            case "salir":
                Application.Quit();
                break;

        }
    }
    public void VolverCambiarNombre()
    {
        if (PlayerPrefs.HasKey(MainManager.KEY_NAME))
        {
            btnVolverNombre.SetActive(true);
        }
        else
        {
            btnVolverNombre.SetActive(false);
        }
    }
    public void BuscaGuardado()
    {
        GameObject main = GameObject.Find("MainManager");
        main.GetComponent<MainManager>().SaveName();
    }
    public void BuscarRecarga()
    {
        GameObject main = GameObject.Find("MainManager");
        main.GetComponent<MainManager>().RestoreName();
    }
    public void BuscarVolumen()
    {
        GameObject main = GameObject.Find("MainManager");
        main.GetComponent<MainManager>().SaveAudioVolume();
    }
}
