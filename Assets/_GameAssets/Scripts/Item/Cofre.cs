using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofre : MonoBehaviour
{
    public GameObject prefabHumo;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool("Abrir", true);
        }
    }
    public void Destruir()
    {
        Instantiate(prefabHumo, transform.position, Quaternion.identity);
        GetComponent<DropeoItem>().SoltarObjeto();
        Destroy(gameObject);
    }
}
