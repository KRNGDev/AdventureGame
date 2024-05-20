using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    
    public  string nombreUbicacion;
    public List<Item> inventarioItem = new List<Item>();

    public List<Item> inventarioEquipacion = new List<Item>();

    public Transform panelContenItem;
    public Transform panelContenEquipo;
    public GameObject prefabItemPanel;
    public TextMeshProUGUI nombre;
    public bool condicion = false;
    
    
    
    [Header("Recuento de puntos")]
    public int puntosActuales = 0;
    public int puntosMax;
    public int puntos;
    private TextMeshProUGUI textoPunto;


    void Start()
    {
        nombre = GameObject.Find("NombreTexto").GetComponent<TextMeshProUGUI>();
        nombre.text = PlayerPrefs.GetString("Nombre");

    }


    public void CargarObjetos()
    {
        print("busca item panel");
        List<Item> copiaInventarioItem = new List<Item>(inventarioItem);
        foreach (Item item in copiaInventarioItem)
        {
            print("busca item panel");
            Debug.Log(item);
            // Busca si ya hay un panel creado para el item
            GameObject panel = BuscarPanelExistente(item.nombre, panelContenItem);

            // Si no hay un panel existente, crea uno nuevo
            if (panel == null)
            {
                print("crea panel");
                CrearPanelItem(item);
                inventarioItem.Remove(item);

            }
            else
            {
                panel.GetComponent<Item>().cantidadItem += item.cantidadItem;
                int cadidadActual = panel.GetComponent<Item>().cantidadItem;
                panel.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "" + cadidadActual;
                inventarioItem.Remove(item);
            }

        }
        List<Item> copiaInventarioEquipacion = new List<Item>(inventarioEquipacion);
        foreach (Item equipo in copiaInventarioEquipacion)
        {
            print("busca item panel");
            GameObject panel = BuscarPanelExistente(equipo.nombre, panelContenEquipo);

            // Si no hay un panel existente, crea uno nuevo
            if (panel == null)
            {
                print("crea panel");

                CrearPanelEquipo(equipo);
                inventarioEquipacion.Remove(equipo);

            }
            else
            {

                panel.GetComponent<Item>().cantidadItem += equipo.cantidadItem;
                int cadidadActual = panel.GetComponent<Item>().cantidadItem;
                panel.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "" + cadidadActual;
                inventarioEquipacion.Remove(equipo);
            }

        }
        MostrarPrimerObjeto();
    }
    public void CrearPanelItem(Item item)
    {

        GameObject panelItem = Instantiate(prefabItemPanel, panelContenItem);

        panelItem.GetComponent<Item>().nombre = item.nombre;
        panelItem.GetComponent<Item>().descripcion = item.descripcion;
        panelItem.GetComponent<Item>().objeto = item.objeto;
        panelItem.GetComponent<Item>().equipo = item.equipo;
        panelItem.GetComponent<Item>().cantidadItem = item.cantidadItem;
        panelItem.GetComponent<Item>().imagen = item.imagen;
        // Obtiene los componentes de texto e imagen del panel
        panelItem.GetComponentsInChildren<Image>()[1].sprite = panelItem.GetComponent<Item>().imagen;
        panelItem.GetComponentInChildren<TextMeshProUGUI>().text = panelItem.GetComponent<Item>().nombre;
        panelItem.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "" + panelItem.GetComponent<Item>().cantidadItem;
        panelItem.name = item.nombre;
    }
    void CrearPanelEquipo(Item item)
    {
        GameObject panelItem = Instantiate(prefabItemPanel, panelContenEquipo);
        // Obtiene los componentes de texto e imagen del panel
        panelItem.GetComponent<Item>().nombre = item.nombre;
        panelItem.GetComponent<Item>().descripcion = item.descripcion;
        panelItem.GetComponent<Item>().objeto = item.objeto;
        panelItem.GetComponent<Item>().equipo = item.equipo;
        panelItem.GetComponent<Item>().cantidadItem = item.cantidadItem;
        panelItem.GetComponent<Item>().imagen = item.imagen;
        // Obtiene los componentes de texto e imagen del panel
        panelItem.GetComponentsInChildren<Image>()[1].sprite = panelItem.GetComponent<Item>().imagen;
        panelItem.GetComponentInChildren<TextMeshProUGUI>().text = panelItem.GetComponent<Item>().nombre;
        panelItem.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "" + panelItem.GetComponent<Item>().cantidadItem;
        panelItem.name = item.nombre;


    }

    public void MostrarDescripcion()
    {

        Item panelItem = EventSystem.current.currentSelectedGameObject.GetComponent<Item>();
        GameObject panel = GameObject.Find("GameManager").GetComponent<UiManager>().panelDescripcion;
        panel.GetComponentsInChildren<TextMeshProUGUI>()[0].text = panelItem.nombre;
        panel.GetComponentsInChildren<TextMeshProUGUI>()[2].text = panelItem.descripcion;
        panel.GetComponentsInChildren<Image>()[3].sprite = panelItem.imagen;


    }
    public void MostrarPrimerObjeto()
    {
        if (panelContenItem.transform.childCount <= 0) return;
        Transform primerObjeto = panelContenItem.transform.GetChild(0);
        Item panelItem = primerObjeto.GetComponent<Item>();
        GameObject panel = GameObject.Find("GameManager").GetComponent<UiManager>().panelDescripcion;
        panel.GetComponentsInChildren<TextMeshProUGUI>()[0].text = panelItem.nombre;
        panel.GetComponentsInChildren<TextMeshProUGUI>()[2].text = panelItem.descripcion;
        panel.GetComponentsInChildren<Image>()[3].sprite = panelItem.imagen;


    }
    public void MostrarPrimerEquipo()
    {
        if (panelContenEquipo.transform.childCount <= 0) return;
        Transform primerObjeto = panelContenEquipo.transform.GetChild(0);
        Item panelItem = primerObjeto.GetComponent<Item>();
        GameObject panel = GameObject.Find("GameManager").GetComponent<UiManager>().panelDescripcion;
        panel.GetComponentsInChildren<TextMeshProUGUI>()[0].text = panelItem.nombre;
        panel.GetComponentsInChildren<TextMeshProUGUI>()[2].text = panelItem.descripcion;
        panel.GetComponentsInChildren<Image>()[3].sprite = panelItem.imagen;



    }

    public GameObject BuscarPanelExistente(String item, Transform panelContenedor)
    {

        // Itera sobre los paneles hijos del contenedor y busca un panel con el mismo nombre que el item
        foreach (Transform child in panelContenedor)
        {
            if (child.name == item)
            {
                return child.gameObject;
            }
        }
        return null; // Retorna null si no se encuentra un panel existente para el item
    }

    public void SumarPuntos()
    {
        print("Sumar");
        textoPunto = GameObject.Find("TextoPuntos").GetComponent<TextMeshProUGUI>();
        print(textoPunto.text);
        puntosActuales++;
        textoPunto.text = "" + puntosActuales;

    }

}