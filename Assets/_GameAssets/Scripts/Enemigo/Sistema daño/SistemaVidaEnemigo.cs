using System;
using System.Collections;
using System.Collections.Generic;
using Enemigo;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SistemaVidaEnemigo : MonoBehaviour
{
    [Header("Datos Personaje")]

    public String tagEnemigo;
    private Animator animator;

    [Header("Fx Personaje")]
    public GameObject humo;
    public bool muerto = false;
    private bool dado = false;
    public bool ataqueArea = false;
    public Slider sliderVida;


    void Start()
    {
        animator = GetComponent<Animator>();

    }
    void Update()
    {
        sliderVida.maxValue = GetComponent<Stats>().vidaMax;
        sliderVida.value = GetComponent<Stats>().vidaActual;
        if (GetComponent<Stats>().vidaActual <= 0 && !muerto)
        {
            muerto = true;

            animator.SetBool("muerto", true);
        }
    }
    public void QuitarVida(float hit)
    {

        GetComponent<Stats>().vidaActual -= hit;


    }
    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {

            case "Espada":
                float damage = other.GetComponentInParent<Stats>().damage;
                QuitarVida(damage);
                animator.SetBool("Dado", true);
                dado = true;
                break;
            case "EspadaFuego":
                float fuego = other.GetComponentInParent<Stats>().damageEspecial;
                QuitarVida(fuego);
                GetComponent<Fx>().FxEnemigo("fuego");
                if (GetComponent<Stats>().vidaActual >= 0 && !muerto)
                {
                    animator.SetBool("caido", true);
                    animator.SetBool("ataca", false);
                    dado = true;
                }

                break;
            case "EspadaHielo":
                float hielo = other.GetComponentInParent<Stats>().damageEspecial;
                QuitarVida(hielo);
                GetComponent<Fx>().FxEnemigo("hielo");

                if (GetComponent<Stats>().vidaActual >= 0 && !muerto)
                {
                    animator.SetBool("caido", true);
                    animator.SetBool("ataca", false);
                    dado = true;
                }
                break;
            case "AtaqueArea":

                float damageArea = other.GetComponentInParent<Stats>().damageArea;
                QuitarVida(damageArea);
                GetComponent<Fx>().FxEnemigo("electric");
                if (GetComponent<Stats>().vidaActual >= 0 && !muerto)
                {

                    animator.SetBool("caido", true);
                    animator.SetBool("ataca", false);
                    dado = true;

                }
                break;
            case "DisparoSlash":
                float slash = other.GetComponentInParent<Stats>().damageDisparo;
                QuitarVida(slash);


                if (GetComponent<Stats>().vidaActual >= 0 && !muerto)
                {
                    animator.SetBool("caido", true);
                    animator.SetBool("ataca", false);
                    dado = true;
                }
                break;

        }


    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(tagEnemigo))
        {
            dado = false;

            animator.SetBool("Dado", false);
        }

    }


    public void DePie()
    {
        dado = false;

        animator.SetBool("caido", false);

    }

    public void Quieto()
    {
        // MonoBehaviour esto coje todo los script del jugador y los desactiva

        MonoBehaviour[] mb = GetComponentsInChildren<MonoBehaviour>();
        GetComponent<NavMeshAgent>().enabled = false;
        foreach (MonoBehaviour m in mb)
        {
            m.enabled = false;
        }
    }
    public void EnMovimiento()
    {
        // MonoBehaviour esto coje todo los script del jugador y los desactiva

        MonoBehaviour[] mb = GetComponentsInChildren<MonoBehaviour>();
        GetComponent<NavMeshAgent>().enabled = true;


        foreach (MonoBehaviour m in mb)
        {
            m.enabled = true;
        }
    }
    public void Destruir()
    {
        GetComponent<DropeoItem>().SoltarObjeto();
        Instantiate(humo, transform.position, transform.rotation);
        Destroy(transform.gameObject);
    }
    public void QuitarGolpe()
    {
        dado = false;
        animator.SetBool("Dado", false);
    }



}


