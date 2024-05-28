using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Borrar : MonoBehaviour
{
    public void CargarIntro()
    {
        SceneManager.LoadScene("Intro");
    }
    public void CargarEscena(string nombre)
    {
        GameObject escene = GameObject.Find("EsceneManager");
        escene.GetComponent<EsceneManager>().CargarEscena(nombre);
    }
}

