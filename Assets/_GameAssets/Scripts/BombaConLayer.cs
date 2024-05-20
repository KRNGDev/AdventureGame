using System.Collections;
using System.Collections.Generic;
//using Enemigo;
using UnityEngine;

public class BombaConLayer : MonoBehaviour
{
    [Header("Radio de acción de la explosión en metros")]
    public float radio;
    public float fuerzaHorizontal;
    public float fuerzaVertical;
    public int damage = 10;

    [Header("Segundos de espera antes de la explosión")]
    public int temporizador;

    public LayerMask layerMask;
    void Start()
    {
        Invoke("Explotar", temporizador);
    }

    void Explotar()
    {
        //Obtiene los colliders afectados por la explosión
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radio, layerMask);
        //Recorre los colliders y a aquellos que tienen RigidBody les aplica una explosión de fuerza
        foreach (var collider in hitColliders)

        {
            /* if (collider.GetComponent<SistemaVidaEnemigo>() != null)
             {
                 collider.GetComponent<SistemaVidaEnemigo>().QuitarVida(damage);
             }*/

            if (collider.GetComponent<Rigidbody>() != null)
            {


                collider.GetComponent<Rigidbody>().AddExplosionForce(
                    fuerzaHorizontal,
                    transform.position,
                    radio,
                    fuerzaVertical);

            }/*
            if (collider.GetComponent<enemigo.Enemigo>() != null)
            {
                collider.GetComponent<enemigo.Enemigo>().congelado=true;
            }
            if (collider.GetComponent<enemigo.EnemigoKamikace>() != null)
            {
                collider.GetComponent<enemigo.EnemigoKamikace>().congelado=true;
            }*/
        }

    }


}
