using System;
using TMPro;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private GameObject player;
    private GameObject panel;
    public bool espada;
    public bool escudo;
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void AtributoItem(String nombre, bool tipo)
    {
        TextMeshProUGUI[] textMeshes = GetComponent<UiManager>().panelDescripcion.GetComponentsInChildren<TextMeshProUGUI>();
        if (textMeshes.Length > 0 && !string.IsNullOrEmpty(textMeshes[0].text))
        {
            string textoNombre = textMeshes[0].text;

            if (tipo == true)
            {
                panel = GetComponent<GameManager>().BuscarPanelExistente(textoNombre, GetComponent<GameManager>().panelContenItem);
            }
            else
            {
                panel = GetComponent<GameManager>().BuscarPanelExistente(textoNombre, GetComponent<GameManager>().panelContenEquipo);

            }


        }
        switch (nombre)
        {
            case "Botella Vida":
                int pv = 5;
                if (player.GetComponent<Stats>().vidaActual == player.GetComponent<Stats>().vidaMax)
                {
                    GetComponent<UiManager>().panelNoUsarMas.SetActive(true);
                }
                else
                if ((player.GetComponent<Stats>().vidaActual + pv) > player.GetComponent<Stats>().vidaMax)
                {
                    panel.GetComponent<Item>().cantidadItem -= 1;
                    panel.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "" + panel.GetComponent<Item>().cantidadItem;

                    player.GetComponent<Stats>().vidaActual = player.GetComponent<Stats>().vidaMax;
                }
                else
                {
                    panel.GetComponent<Item>().cantidadItem -= 1;
                    panel.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "" + panel.GetComponent<Item>().cantidadItem;

                    player.GetComponent<Stats>().vidaActual += pv;
                }
                break;

            case "Botella Mana":
                int pm = 5;
                if (player.GetComponent<Stats>().manaActual == player.GetComponent<Stats>().manaMax)
                {
                    GetComponent<UiManager>().panelNoUsarMas.SetActive(true);
                }
                else
                if ((player.GetComponent<Stats>().manaActual + pm) > player.GetComponent<Stats>().manaMax)
                {
                    panel.GetComponent<Item>().cantidadItem -= 1;
                    panel.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "" + panel.GetComponent<Item>().cantidadItem;
                    player.GetComponent<Stats>().manaActual = player.GetComponent<Stats>().manaMax;
                }
                else
                {
                    panel.GetComponent<Item>().cantidadItem -= 1;
                    panel.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "" + panel.GetComponent<Item>().cantidadItem;
                    player.GetComponent<Stats>().manaActual += pm;
                }
                break;
            case "Espada Corta":
                if (!espada)
                {
                    GetComponent<EquipoManager>().espadaCorta.SetActive(true);
                    panel.GetComponentsInChildren<TextMeshProUGUI>()[3].text = "Equipado";
                    espada = true;
                    GetComponent<GameManager>().condicion = true;
                }
                else
                {
                    GetComponent<UiManager>().panelYaEstaEquipado.SetActive(true);
                }
                break;
            case "Espada Maestra":
                if (!espada)
                {
                    GetComponent<EquipoManager>().espadaMaestra.SetActive(true);
                    panel.GetComponentsInChildren<TextMeshProUGUI>()[3].text = "Equipado";
                    espada = true;
                }
                else
                {
                    GetComponent<UiManager>().panelYaEstaEquipado.SetActive(true);
                }
                break;
            case "Escudo Basico":
                GetComponent<EquipoManager>().escudoBasico.SetActive(true);
                panel.GetComponentsInChildren<TextMeshProUGUI>()[3].text = "Equipado";
                escudo = true;
                break;


            default:
                break;
        }
    }
    public void QuitarEquipo(String nombre, bool tipo)
    {
        TextMeshProUGUI[] textMeshes = GetComponent<UiManager>().panelDescripcion.GetComponentsInChildren<TextMeshProUGUI>();
        if (textMeshes.Length > 0 && !string.IsNullOrEmpty(textMeshes[0].text))
        {
            string textoNombre = textMeshes[0].text;

            if (tipo == true)
            {
                panel = GetComponent<GameManager>().BuscarPanelExistente(textoNombre, GetComponent<GameManager>().panelContenItem);
            }
            else
            {
                panel = GetComponent<GameManager>().BuscarPanelExistente(textoNombre, GetComponent<GameManager>().panelContenEquipo);
                print(panel.name);
            }


        }
        switch (nombre)
        {
            case "Espada Corta":
                if (espada)
                {
                    GetComponent<EquipoManager>().espadaCorta.SetActive(false);
                    panel.GetComponentsInChildren<TextMeshProUGUI>()[3].text = "";
                    espada = false;
                }

                break;
            case "Espada Maestra":
                if (espada)
                {
                    GetComponent<EquipoManager>().espadaMaestra.SetActive(false);
                    panel.GetComponentsInChildren<TextMeshProUGUI>()[3].text = "";
                    espada = false;
                }

                break;
            case "Escudo Basico":
                GetComponent<EquipoManager>().escudoBasico.SetActive(false);
                panel.GetComponentsInChildren<TextMeshProUGUI>()[3].text = "";
                escudo = false;
                break;
            default:
                break;
        }
    }

}
