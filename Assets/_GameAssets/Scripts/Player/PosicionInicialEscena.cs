using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PosicionInicialEscena : MonoBehaviour
{
    private GameObject player;
    private string posicion;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        PosicionPlayer();
    }

    public void PosicionPlayer()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        posicion = gm.nombreUbicacion;

        if (posicion != null)
        {

            player.transform.position = GameObject.Find(posicion).transform.position;

        }



    }

}
