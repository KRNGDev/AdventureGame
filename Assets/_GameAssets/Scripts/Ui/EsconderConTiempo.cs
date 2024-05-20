using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsconderConTiempo : MonoBehaviour
{
    public float tiempoOcultar = 1.5f;
    public GameObject gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
        StartCoroutine(DestruirDespuesDe(tiempoOcultar));
    }

    IEnumerator DestruirDespuesDe(float tiempo)
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(tiempo);

        gm.GetComponent<UiManager>().panelNoUsarMas.SetActive(false);
    }
}
