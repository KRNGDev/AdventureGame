using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public string nombreUbicacion;
    public List<Item> inventarioItem = new List<Item>();
    public List<Item> inventarioEquipacion = new List<Item>();
    public Transform panelContenItem;
    public Transform panelContenEquipo;
    public GameObject prefabItemPanel;
    public TextMeshProUGUI nombre;
    public bool condicion = false;
    public bool espada = false;
    private GameObject player;

    [Header("Recuento de puntos")]
    public int puntosActuales = 0;
    public int puntosMax;
    public int puntos;
    private TextMeshProUGUI textoPunto;

    void Start()
    {
        player = GameObject.Find("Player");
        nombre = GameObject.Find("NombreTexto").GetComponent<TextMeshProUGUI>();
        nombre.text = PlayerPrefs.GetString("Nombre");
    }

    void Update()
    {
        // Mantener este método vacío o removerlo si no se usa.
    }

    public void CargarObjetos()
    {
        ProcesarInventario(inventarioItem, panelContenItem, CrearPanelItem);
        ProcesarInventario(inventarioEquipacion, panelContenEquipo, CrearPanelEquipo);
        MostrarPrimerObjeto();
    }

    private void ProcesarInventario(List<Item> inventario, Transform panelContenedor, Action<Item> crearPanel)
    {
        List<Item> copiaInventario = new List<Item>(inventario);
        foreach (Item item in copiaInventario)
        {
            GameObject panel = BuscarPanelExistente(item.nombre, panelContenedor);
            if (panel == null)
            {
                crearPanel(item);
            }
            else
            {
                Item panelItem = panel.GetComponent<Item>();
                panelItem.cantidadItem += item.cantidadItem;
                panel.GetComponentsInChildren<TextMeshProUGUI>()[1].text = panelItem.cantidadItem.ToString();
            }
            inventario.Remove(item);
        }
    }

    private void CrearPanelItem(Item item)
    {
        GameObject panelItem = Instantiate(prefabItemPanel, panelContenItem);
        ConfigurarPanelItem(panelItem, item);
    }

    private void CrearPanelEquipo(Item item)
    {
        GameObject panelItem = Instantiate(prefabItemPanel, panelContenEquipo);
        ConfigurarPanelItem(panelItem, item);
    }

    private void ConfigurarPanelItem(GameObject panelItem, Item item)
    {
        Item panelItemComponent = panelItem.GetComponent<Item>();
        panelItemComponent.nombre = item.nombre;
        panelItemComponent.descripcion = item.descripcion;
        panelItemComponent.objeto = item.objeto;
        panelItemComponent.equipo = item.equipo;
        panelItemComponent.cantidadItem = item.cantidadItem;
        panelItemComponent.imagen = item.imagen;

        panelItem.GetComponentsInChildren<Image>()[1].sprite = item.imagen;
        panelItem.GetComponentInChildren<TextMeshProUGUI>().text = item.nombre;
        panelItem.GetComponentsInChildren<TextMeshProUGUI>()[1].text = item.cantidadItem.ToString();
        panelItem.name = item.nombre;
    }

    public void MostrarDescripcion()
    {
        Item panelItem = EventSystem.current.currentSelectedGameObject.GetComponent<Item>();
        GameObject panel = GameObject.Find("GameManager").GetComponent<UiManager>().panelDescripcion;
        ConfigurarPanelDescripcion(panel, panelItem);
    }

    private void ConfigurarPanelDescripcion(GameObject panel, Item panelItem)
    {
        panel.GetComponentsInChildren<TextMeshProUGUI>()[0].text = panelItem.nombre;
        panel.GetComponentsInChildren<TextMeshProUGUI>()[2].text = panelItem.descripcion;
        panel.GetComponentsInChildren<Image>()[3].sprite = panelItem.imagen;
    }

    public void MostrarPrimerObjeto()
    {
        if (panelContenItem.childCount > 0)
        {
            Transform primerObjeto = panelContenItem.GetChild(0);
            Item panelItem = primerObjeto.GetComponent<Item>();
            GameObject panel = GameObject.Find("GameManager").GetComponent<UiManager>().panelDescripcion;
            ConfigurarPanelDescripcion(panel, panelItem);
        }
    }

    public void MostrarPrimerEquipo()
    {
        if (panelContenEquipo.childCount > 0)
        {
            Transform primerObjeto = panelContenEquipo.GetChild(0);
            Item panelItem = primerObjeto.GetComponent<Item>();
            GameObject panel = GameObject.Find("GameManager").GetComponent<UiManager>().panelDescripcion;
            ConfigurarPanelDescripcion(panel, panelItem);
        }
    }

    public GameObject BuscarPanelExistente(string item, Transform panelContenedor)
    {
        foreach (Transform child in panelContenedor)
        {
            if (child.name == item)
            {
                return child.gameObject;
            }
        }
        return null;
    }

    public void SumarPuntos()
    {
        ActualizarPuntos(1);
    }

    public void QuitarPuntos()
    {
        ActualizarPuntos(-1);
    }

    private void ActualizarPuntos(int cantidad)
    {
        puntosActuales += cantidad;
        textoPunto = GameObject.Find("TextoPuntos").GetComponent<TextMeshProUGUI>();
        textoPunto.text = puntosActuales.ToString();
    }
    public void BuscarCargarEscena(string nombre)
    {
        GameObject escene = GameObject.Find("EsceneManager");
        escene.GetComponent<EsceneManager>().CargarEscena(nombre);
    }
}
