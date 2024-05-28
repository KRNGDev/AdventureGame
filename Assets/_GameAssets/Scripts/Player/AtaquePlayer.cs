using System.Collections;
using System.Collections.Generic;
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
        CheckManaAndStartCooldowns();
    }

    private void CheckManaAndStartCooldowns()
    {
        Stats playerStats = GetComponent<Stats>();
        float manaActual = playerStats.manaActual;

        UpdateCooldown(manaActual <= 1, ref comprobarManaEspecial, ref cargandoEspecial, imagenEspecial, "TiempoEspecial", timeEspecial);
        UpdateCooldown(manaActual <= 2, ref comprobarManaDisparo, ref cargandoDisparo, imagenDisparo, "TiempoDisparo", timeDisparo);
        UpdateCooldown(manaActual <= 4, ref comprobarManaArea, ref cargandoArea, imagenArea, "TiempoArea", timeArea);
    }

    private void UpdateCooldown(bool manaCondition, ref bool manaCheck, ref bool cargando, Image imagen, string methodName, float time)
    {
        if (manaCondition)
        {
            imagen.fillAmount = 0;
            manaCheck = false;
        }
        else if (!manaCheck)
        {
            manaCheck = true;
            cargando = true;
            InvokeRepeating(methodName, 0, time / 100f);
        }
    }

    public void Ataque()
    {
        if (!cargandoAtaque && gm.GetComponent<ItemManager>().espada)
        {
            StartAtaque();
        }
    }

    private void StartAtaque()
    {
        GetComponent<ControlPlayer>().atacando = true;
        cargandoAtaque = true;
        imagenAtaque.fillAmount = 0;
        animator.SetBool("Ataca", true);
        InvokeRepeating("TiempoAtaque", 0, timeAtaque / 100f);
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

    public void FinDistancia()
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
        if (!cargandoEspecial && gm.GetComponent<ItemManager>().espada && GetComponent<Stats>().manaActual > 1)
        {
            StartEspecial();
        }
    }

    private void StartEspecial()
    {
        imagenEspecial.fillAmount = 0;
        animator.SetBool("ataqueGiratorio", true);
    }

    public void AtaqueDistancia()
    {
        if (!cargandoDisparo && gm.GetComponent<ItemManager>().espada && GetComponent<Stats>().manaActual > 2)
        {
            StartDistancia();
        }
    }

    private void StartDistancia()
    {
        imagenDisparo.fillAmount = 0;
        animator.SetBool("ataqueDistancia", true);
    }

    public void AtaqueArea()
    {
        if (!cargandoArea && GetComponent<Stats>().manaActual > 4)
        {
            StartArea();
        }
    }

    private void StartArea()
    {
        GetComponent<ControlPlayer>().atacando = true;
        imagenArea.fillAmount = 0;
        animator.SetBool("ataqueArea", true);
    }

    public void SlashActivo()
    {
        int gastoMana = 2;
        if (GetComponent<Stats>().manaActual >= gastoMana)
        {
            StartSlash(gastoMana);
        }
    }

    private void StartSlash(int gastoMana)
    {
        InvokeRepeating("TiempoEspecial", 0, timeEspecial / 100f);
        cargandoEspecial = true;
        if (skillFuego)
        {
            CreateSkillEffect(corteFuego, "slashFuego", gastoMana);
        }
        else if (skillHielo)
        {
            CreateSkillEffect(corteHielo, "slashHielo", gastoMana);
        }
    }

    private void CreateSkillEffect(GameObject skillPrefab, string soundName, int manaCost)
    {
        GetComponent<SoundManager>().FxSonido(soundName);
        GameObject skillEffect = Instantiate(skillPrefab, spawnHabilidad.position, spawnHabilidad.rotation);
        skillEffect.transform.parent = transform;
        GetComponent<Stats>().manaActual -= manaCost;
    }

    public void DisparoSlash()
    {
        int gastoMana = 3;
        if (GetComponent<Stats>().manaActual >= gastoMana)
        {
            StartDisparo(gastoMana);
        }
    }

    private void StartDisparo(int gastoMana)
    {
        InvokeRepeating("TiempoDisparo", 0, timeDisparo / 100f);
        cargandoDisparo = true;
        GameObject disparoSlash = Instantiate(slash, spawnSlash.position, spawnSlash.rotation);
        disparoSlash.GetComponent<Rigidbody>().velocity = spawnSlash.forward * velDisparo;
        GetComponent<Stats>().manaActual -= gastoMana;
    }

    public void ExplosionArea()
    {
        int gastoMana = 5;
        if (GetComponent<Stats>().manaActual >= gastoMana)
        {
            StartExplosion(gastoMana);
        }
    }

    private void StartExplosion(int gastoMana)
    {
        InvokeRepeating("TiempoArea", 0, timeArea / 100f);
        cargandoArea = true;
        GameObject explosionArea = Instantiate(exploArea, spawnHabilidad.position, transform.rotation);
        explosionArea.transform.parent = transform;
        GetComponent<Stats>().manaActual -= gastoMana;
    }

    public void TiempoAtaque()
    {
        UpdateCooldownProgress(imagenAtaque, ref cargandoAtaque, "TiempoAtaque", 5f);
    }

    public void TiempoEspecial()
    {
        UpdateCooldownProgress(imagenEspecial, ref cargandoEspecial, "TiempoEspecial", 1f);
    }

    public void TiempoDisparo()
    {
        UpdateCooldownProgress(imagenDisparo, ref cargandoDisparo, "TiempoDisparo", 2f);
    }

    public void TiempoArea()
    {
        UpdateCooldownProgress(imagenArea, ref cargandoArea, "TiempoArea", 1f);
    }

    private void UpdateCooldownProgress(Image imagen, ref bool cargando, string methodName, float increment)
    {
        imagen.fillAmount += increment / 360f;
        if (imagen.fillAmount >= 1f)
        {
            cargando = false;
            CancelInvoke(methodName);
        }
    }
}
