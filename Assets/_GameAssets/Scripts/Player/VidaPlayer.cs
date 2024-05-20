using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class VidaPlayer : MonoBehaviour
{
    [Header("Datos Personaje")]
    public bool inmortal = false;

    public String tagEnemigo;


    public GameObject panelGameOver;
    private bool dado = false;

private void Start() {
    panelGameOver=GameObject.Find("PanelGameOver");
    panelGameOver.SetActive(false);
    
}

    void Update()
    {


        if (GetComponent<Stats>().vidaActual <= 0)
        {
            GameObject gm=GameObject.Find("GameManager");
            gm.GetComponent<UiManager>().controller.SetActive(false);

            GetComponent<Animator>().SetBool("muerto", true);
            panelGameOver.SetActive(true);

        }
    }
    public void QuitarVida(int hit)
    {
        if (!inmortal)
        {
            GetComponent<Stats>().vidaActual -= hit;
        }




    }
    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag(tagEnemigo) && !dado)
        {


            int damage = 2;//enemigo.puntosDano;
            QuitarVida(damage);
            GetComponent<Animator>().SetBool("Dado", true);
            dado = true;
            GetComponent<ControlPlayer>().enabled = false;




        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(tagEnemigo))
        {
            dado = false;
            GetComponent<Animator>().SetBool("Dado", false);

        }
    }
    public void QuitarGolpe()
    {
        dado = false;
        GetComponent<Animator>().SetBool("Dado", false);
        GetComponent<ControlPlayer>().enabled = true;
    }
    public void GameOver()
    {
        Time.timeScale = 0;
    }

}
