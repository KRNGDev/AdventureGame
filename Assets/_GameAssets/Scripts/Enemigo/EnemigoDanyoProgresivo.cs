using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoDanyoProgresivo : MonoBehaviour
{
    [Header("Daño infringido en cada lapso de tiempo")]
    public int danyo;
    [Header("Tiempo transcurrido entre cada incremento/decremento daño")]
    public float frecuencia;

    private Transform transformPlayer;
    public void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            transformPlayer = c.gameObject.transform;
            InvokeRepeating("HacerDanyo", 0, frecuencia);
        }
    }
    public void OnTriggerExit(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            CancelInvoke("HacerDanyo");
        }
    }
    private void HacerDanyo()
    {
        // transformPlayer.gameObject.GetComponent<SistemaVidaPlayer>()?.QuitarVida(danyo);
    }
}
