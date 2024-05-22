using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DropeoItem : MonoBehaviour
{
    public GameObject[] item;
    public int alturaDrop = -1;
    public void SoltarObjeto()
    {
        int itemAleatorio = Random.Range(0, item.Length - 1);
        //Vector3 elevacion = new Vector3(0, alturaDrop, 0);
        Vector3 posicion = transform.position;

        Instantiate(item[itemAleatorio], posicion, transform.rotation);
    }
}
