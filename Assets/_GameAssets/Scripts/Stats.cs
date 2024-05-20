using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stats : MonoBehaviour
{
    public float vidaMax;
    public float vidaActual;
    public float manaMax;
    public float manaActual;
    public float damage;
    public float damageEspecial;
    public float damageArea;
    public float damageDisparo;

    public Image barraVida;
    public Image barraMana;

    void Start()
    {
        vidaActual = vidaMax;
        manaActual = manaMax;
    }
    void Update()
    {
        if (manaActual <= 0)
        {
            manaActual = 0;
        }
        if (barraMana || barraVida)
        {
            barraVida.fillAmount = vidaActual / vidaMax;
            barraMana.fillAmount = manaActual / manaMax;
        }
    }
}
