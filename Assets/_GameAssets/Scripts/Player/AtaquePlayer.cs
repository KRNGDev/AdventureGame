using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class AtaquePlayer : MonoBehaviour
{

    [Header("Datos Player")]
    public int damage;
    public int damageSkill;
    public int damageSlash;
    public int damageArea;
    [Header("Datos Habilidades")]
    public float timeAtaque = 0.1f;
    public float timeEspecial = 0.5f;
    public float timeArea = 1;
    public float timeDisparo = 0.3f;
    public GameObject corteFuego;
    public GameObject corteHielo;
    public GameObject slash;
    public GameObject exploArea;
    public Image imagenAtaque;
    public Image imagenEspecial;
    public Image imagenArea;
    public Image imagenDisparo;
    public Transform spawnHabilidad;
    public Transform spawnSlash;

    [Header("Habilidades activas")]
    public float velDisparo = 10;
    public bool cargandoAtaque;
    public bool cargandoEspecial;
    public bool cargandoArea;
    public bool cargandoDisparo;
    private bool comprobarManaEspecial;
    private bool comprobarManaDisparo;
    private bool comprobarManaArea;
    public bool skillFuego = false;
    public bool skillHielo = false;

    [Header("Estados Player")]
    public bool estaEnSuelo = true;

    private Animator animator;
    private GameObject gm;
    void Start()
    {

        animator = GetComponent<Animator>();
        gm = GameObject.Find("GameManager");


    }
    void Update()
    {

        if (GetComponent<Stats>().manaActual <= 1)
        {
            imagenEspecial.fillAmount = 0;
            comprobarManaEspecial = false;
        }
        else
        {
            if (!comprobarManaEspecial)
            {
                comprobarManaEspecial = true;
                cargandoEspecial = true;
                InvokeRepeating("TiempoEspecial", 0, (timeEspecial / 100f));
            }
        }
        if (GetComponent<Stats>().manaActual <= 2)
        {
            imagenDisparo.fillAmount = 0;
        }
        else
        {
            if (!comprobarManaDisparo)
            {
                comprobarManaDisparo = true;
                cargandoDisparo = true;
                InvokeRepeating("TiempoDisparo", 0, (timeDisparo / 100f));
            }

        }
        if (GetComponent<Stats>().manaActual <= 4)
        {
            imagenArea.fillAmount = 0;
        }
        else
        {
            if (!comprobarManaArea)
            {
                comprobarManaArea = true;
                cargandoArea = true;
                InvokeRepeating("TiempoArea", 0, (timeArea / 100f));

            }

        }
    }
    public void Ataque()
    {
        if (!cargandoAtaque && gm.GetComponent<ItemManager>().espada == true)
        {
            GetComponent<ControlPlayer>().atacando = true;
            cargandoAtaque = true;
            imagenAtaque.fillAmount = 0;
            animator.SetBool("Ataca", true);
            InvokeRepeating("TiempoAtaque", 0, (timeAtaque / 100f));
        }
    }
    public void FinAtaque()
    {
        animator.SetBool("Ataca", false);
        GetComponent<ControlPlayer>().atacando = false;
    }
    public void FinGiratorio()
    {
        animator.SetBool("ataqueGiratorio", false);
    }
    public void FinDistacia()
    {
        animator.SetBool("ataqueDistancia", false);
    }
    public void FinArea()
    {
        GetComponent<ControlPlayer>().atacando = false;
        animator.SetBool("ataqueArea", false);
    }
    public void Defensa()
    {
        animator.SetBool("Defendiendo", true);
    }
    public void AtaqueEspecial()
    {
        if (!cargandoEspecial)
        {

            imagenEspecial.fillAmount = 0;
            animator.SetBool("ataqueGiratorio", true);
        }
        if (GetComponent<Stats>().manaActual <= 1)
        {

            animator.SetBool("ataqueGiratorio", true);
        }

    }
    public void AtaqueDistancia()
    {
        if (!cargandoDisparo)
        {

            imagenDisparo.fillAmount = 0;
            animator.SetBool("ataqueDistancia", true);
        }
        if (GetComponent<Stats>().manaActual <= 2)
        {

            animator.SetBool("ataqueDistancia", true);
        }
    }
    public void AtaqueArea()
    {
        if (!cargandoArea)
        {
            GetComponent<ControlPlayer>().atacando = true;
            imagenArea.fillAmount = 0;
            animator.SetBool("ataqueArea", true);
        }
        if (GetComponent<Stats>().manaActual <= 4)
        {
            GetComponent<ControlPlayer>().atacando = true;
            animator.SetBool("ataqueArea", true);
        }
    }
    public void SlashActivo()
    {
        int gastoMana = 2;
        if ((GetComponent<Stats>().manaActual - gastoMana) >= 0)
        {
            InvokeRepeating("TiempoEspecial", 0, (timeEspecial / 100f));
            cargandoEspecial = true;
            if (skillFuego)
            {
                GetComponent<SoundManager>().FxSonido("slashFuego");
                GameObject cortFuego = Instantiate(corteFuego, spawnHabilidad.position, spawnHabilidad.rotation);
                cortFuego.transform.parent = transform;
                GetComponent<Stats>().manaActual -= gastoMana;

            }

            if (skillHielo)
            {
                GetComponent<SoundManager>().FxSonido("slashHielo");
                GameObject cortHielo = Instantiate(corteHielo, spawnHabilidad.position, spawnHabilidad.rotation);
                cortHielo.transform.parent = transform;
                GetComponent<Stats>().manaActual -= gastoMana;

            }
        }
        if (GetComponent<Stats>().manaActual < 2)
        {
            imagenEspecial.fillAmount = 0;
        }
    }
    public void DisparoSlash()
    {
        int gastoMana = 3;
        if ((GetComponent<Stats>().manaActual - gastoMana) >= 0)
        {
            InvokeRepeating("TiempoDisparo", 0, (timeDisparo / 100f));
            cargandoDisparo = true;
            GameObject disparoSlash = Instantiate(slash, spawnSlash.position, spawnSlash.rotation);
            disparoSlash.GetComponent<Rigidbody>().velocity = spawnSlash.forward * velDisparo;
            GetComponent<Stats>().manaActual -= gastoMana;
        }
        if (GetComponent<Stats>().manaActual < 3)
        {
            imagenDisparo.fillAmount = 0;
        }
    }
    public void ExplosionArea()
    {
        int gastoMana = 5;
        if ((GetComponent<Stats>().manaActual - gastoMana) >= 0)
        {
            InvokeRepeating("TiempoArea", 0, (timeArea / 100f));
            cargandoArea = true;
            GameObject explosionArea = Instantiate(exploArea, spawnHabilidad.position, transform.rotation);
            explosionArea.transform.parent = transform;
            GetComponent<Stats>().manaActual -= gastoMana;
        }
    }
    public void TiempoAtaque()
    {
        imagenAtaque.fillAmount += 5f / 360f;
        if (imagenAtaque.fillAmount >= 1f)
        {
            cargandoAtaque = false;
            CancelInvoke("TiempoAtaque");
        }
    }
    public void TiempoEspecial()
    {

        imagenEspecial.fillAmount += 1f / 360f;
        if (imagenEspecial.fillAmount >= 1f)
        {
            cargandoEspecial = false;
            CancelInvoke("TiempoEspecial");
        }

    }
    public void TiempoDisparo()
    {
        imagenDisparo.fillAmount += 2f / 360f;
        if (imagenDisparo.fillAmount >= 1f)
        {
            cargandoDisparo = false;
            CancelInvoke("TiempoDisparo");
        }
    }
    public void TiempoArea()
    {
        imagenArea.fillAmount += 1f / 360f;
        if (imagenArea.fillAmount >= 1f)
        {
            cargandoArea = false;
            CancelInvoke("TiempoArea");
        }
    }

}
