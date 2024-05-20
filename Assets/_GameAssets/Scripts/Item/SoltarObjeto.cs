using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoltarObjeto : MonoBehaviour
{
    public float tiempoDeVida = 0.5f; // Tiempo en segundos antes de destruir el objeto

    void Start()
    {
        // Iniciar la corutina de destrucción
        StartCoroutine(DestruirDespuesDe(tiempoDeVida));
    }

    IEnumerator DestruirDespuesDe(float tiempo)
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(tiempo);

        GetComponent<DropeoItem>().SoltarObjeto();

        // Destruir el GameObject
        Destroy(gameObject);
    }
}
