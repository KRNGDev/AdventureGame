using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PosicionInicialEscena : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private string posicion;
    // Start is called before the first frame update
    void Start()
    {

        PosicionPlayer();
    }

    public void PosicionPlayer()
    {
        player = GameObject.Find("Player");
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        posicion = gm.nombreUbicacion;

        if (posicion != null)
        {

            player.transform.position = GameObject.Find(posicion).transform.position;

        }



    }

}
