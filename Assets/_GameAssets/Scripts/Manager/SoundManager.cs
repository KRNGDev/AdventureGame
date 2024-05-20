using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [Header("Sonidos Player")]
    public AudioClip pisada;
    public AudioClip golpeAliento;
    public AudioClip golpeAliento2;
    public AudioClip golpeAlientoLargo;
    public AudioClip gritoArea;
    public AudioClip sonidoEspada;
    public AudioClip damage;
    public AudioClip caida;
    public AudioClip muerto;
    [Header("Sonido Habilidades")]
    public AudioClip slashFuego;
    public AudioClip slashHielo;
    public AudioClip sonidoEsxploArea;
    public AudioClip sonidoEsxploSlash;





    public void FxSonido(string sonido)
    {

        switch (sonido)
        {

            case "golpeAliento":
                GetComponent<AudioSource>().PlayOneShot(golpeAliento);
                break;
            case "golpeAliento2":
                GetComponent<AudioSource>().PlayOneShot(golpeAliento2);
                break;
            case "golpeAlientoLargo":
                GetComponent<AudioSource>().PlayOneShot(golpeAlientoLargo);
                break;
            case "gritoArea":
                GetComponent<AudioSource>().PlayOneShot(gritoArea);
                break;
            case "pisada":
                GetComponent<AudioSource>().PlayOneShot(pisada);
                break;
            case "slashFuego":
                GetComponent<AudioSource>().PlayOneShot(slashFuego);
                break;
            case "slashHielo":
                GetComponent<AudioSource>().PlayOneShot(slashHielo);
                break;
            case "sonidoEsxploArea":
                GetComponent<AudioSource>().PlayOneShot(sonidoEsxploArea);
                break;
            case "sonidoEspada":
                GetComponent<AudioSource>().PlayOneShot(sonidoEspada);
                break;
            case "damage":
                GetComponent<AudioSource>().PlayOneShot(damage);
                break;
            case "caida":
                GetComponent<AudioSource>().PlayOneShot(caida);
                break;
            case "muerto":
                GetComponent<AudioSource>().PlayOneShot(muerto);
                break;
            case "disparoSlash":
                GetComponent<AudioSource>().PlayOneShot(sonidoEsxploSlash);
                break;

        }

    }

}
