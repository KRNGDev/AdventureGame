using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoDestruirEscenas : MonoBehaviour
{
    public bool inicio;
    public static NoDestruirEscenas Instance;
    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }
    }

public void Destruir(){
    Destroy(gameObject);
}

}

