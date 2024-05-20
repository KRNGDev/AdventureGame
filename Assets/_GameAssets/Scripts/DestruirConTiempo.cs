using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirConTiempo : MonoBehaviour
{
    public float tiempoDeVida = 3.0f; // Tiempo en segundos antes de destruir el objeto

    void Start()
    {
        // Iniciar la corutina de destrucción
        StartCoroutine(DestruirDespuesDe(tiempoDeVida));

    }

    IEnumerator DestruirDespuesDe(float tiempo)
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(tiempo);

        // Destruir el GameObject
        Destroy(gameObject);
        // Busca todos los objetos con el script "Dialogos"
        Dialogos[] objetosDialogos = FindObjectsOfType<Dialogos>();

        // Itera sobre cada objeto encontrado
        foreach (Dialogos dialogo in objetosDialogos)
        {
            // Establece la variable hablando en false
            dialogo.hablado = false;
        }
    }
    public void Destruir()
    {
        Destroy(gameObject);
        // Busca todos los objetos con el script "Dialogos"
        Dialogos[] objetosDialogos = FindObjectsOfType<Dialogos>();

        // Itera sobre cada objeto encontrado
        foreach (Dialogos dialogo in objetosDialogos)
        {
            // Establece la variable hablando en false
            dialogo.hablado = false;
        }
    }


}
