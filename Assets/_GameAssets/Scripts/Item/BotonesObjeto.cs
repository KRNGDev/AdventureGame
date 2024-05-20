using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BotonesObjeto : MonoBehaviour
{
    public GameObject btnObjeto;
    public GameObject btnEquipo;

    public void MostarBoton()
    {
        if (GetComponent<Item>().objeto == true)
        {
            btnObjeto.SetActive(true);
        }
        else
        {
            btnEquipo.SetActive(true);
        }
    }
    public void QuitarOtrosBtn()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("BotonesItem");
        foreach (GameObject go in gos)
        {
            go.SetActive(false);
        }


    }
    // Método recursivo para recorrer todos los hijos de un GameObject
    void DesactivarBotonesHijosRecursivo(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            // Verificamos si el hijo tiene el tag "Boton"
            if (child.CompareTag("BotonesItem"))
            {
                // Desactivamos el GameObject si tiene el tag "Boton"
                child.gameObject.SetActive(false);
            }

            // Llamamos recursivamente a este método para seguir buscando en los hijos de este hijo
            // Esto nos permite recorrer toda la jerarquía de hijos del GameObject actual
            DesactivarBotonesHijosRecursivo(child.gameObject);
        }
    }


    public void Usar()
    {
        if (GetComponent<Item>().cantidadItem >= 1)
        {
            ItemManager gm = GameObject.Find("GameManager").GetComponent<ItemManager>();
            string nombre = GetComponentInChildren<TextMeshProUGUI>().text;
            bool tipo = GetComponent<Item>().objeto;
            gm.AtributoItem(nombre, tipo);
            GameObject use = Instantiate(gm.GetComponent<UiManager>().prefabSonido, transform.position, transform.rotation);
            use.GetComponent<AudioSource>().clip = gm.GetComponent<UiManager>().sonidoUsar;
            use.GetComponent<AudioSource>().Play();

            if (GetComponent<Item>().cantidadItem == 0)
            {
                Destroy(gameObject);
            }

        }

    }
    public void Equipar()
    {
        print("Equipa");


        ItemManager gm = GameObject.Find("GameManager").GetComponent<ItemManager>();
        string nombre = GetComponentInChildren<TextMeshProUGUI>().text;
        gm.AtributoItem(nombre, GetComponent<Item>().objeto);
        GameObject equi = Instantiate(gm.GetComponent<UiManager>().prefabSonido, transform.position, transform.rotation);
        equi.GetComponent<AudioSource>().clip = gm.GetComponent<UiManager>().sonidoEquipar;
        equi.GetComponent<AudioSource>().Play();
        print("Equipado");
    }
    public void Quitar()
    {

        ItemManager gm = GameObject.Find("GameManager").GetComponent<ItemManager>();
        string nombre = GetComponentInChildren<TextMeshProUGUI>().text;
        gm.QuitarEquipo(nombre, GetComponent<Item>().objeto);
        GameObject quit = Instantiate(gm.GetComponent<UiManager>().prefabSonido, transform.position, transform.rotation);
        quit.GetComponent<AudioSource>().clip = gm.GetComponent<UiManager>().sonidoQuitar;
        quit.GetComponent<AudioSource>().Play();

    }
}
