using System;
using UnityEngine;

public class VidaPlayer : MonoBehaviour
{
    [Header("Datos Personaje")]
    public bool inmortal = false;
    public string tagEnemigo;

    [Header("Panel Game Over")]
    public GameObject panelGameOver;
    private bool dado = false;

    [Header("Fx Personaje")]
    public GameObject humo;

    private void Start()
    {
        panelGameOver = GameObject.Find("PanelGameOver");
        panelGameOver.SetActive(false);
    }

    private void Update()
    {
        VerificarVida();
    }

    private void VerificarVida()
    {
        if (GetComponent<Stats>().vidaActual <= 0)
        {
            GameObject gm = GameObject.Find("GameManager");
            UiManager uiManager = gm.GetComponent<UiManager>();

            uiManager.controller.SetActive(false);
            uiManager.btnMenu.SetActive(false);

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

    private void OnTriggerEnter(Collider other)
    {
        if (!dado && (other.gameObject.CompareTag(tagEnemigo) || other.gameObject.CompareTag("LanzaLlamas")))
        {
            int damage = other.gameObject.CompareTag(tagEnemigo) ? 2 : 5;
            QuitarVida(damage);

            GetComponent<Animator>().SetBool("Dado", true);
            dado = true;
            GetComponent<ControlPlayer>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(tagEnemigo))
        {
            ResetDado();
        }
    }

    public void QuitarGolpe()
    {
        ResetDado();
        GetComponent<ControlPlayer>().enabled = true;
    }

    private void ResetDado()
    {
        dado = false;
        GetComponent<Animator>().SetBool("Dado", false);
    }

    public void GameOver()
    {
        Instantiate(humo, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
