using UnityEngine;
using TMPro;
using System.Collections;

public class DialogoLargo : MonoBehaviour
{
    [Header("Datos Dialogo")]
    [SerializeField] private GameObject marcaDialogo;
    [SerializeField] private GameObject btnDialogo;
    [SerializeField] private TextMeshProUGUI textoDialogo;
    [SerializeField] private TextMeshProUGUI textoNombre;
    public float tiempoLetras;
    public bool playerEnRango;
    public bool dialogoEmpezado;
    public int lineaIndex;
    [Header("Texto para NPC")]
    public string nombreNPC;
    [SerializeField, TextArea(4, 6)] private string[] lineasDialogo;
    [SerializeField] private GameObject dialogoPanel;

    void Start()
    {
        dialogoPanel = GameObject.Find("PanelConversacion2");
        textoNombre = GameObject.Find("NombreNPC").GetComponent<TextMeshProUGUI>();
        textoDialogo = GameObject.Find("Conversacion").GetComponent<TextMeshProUGUI>();

        dialogoPanel.SetActive(false);
    }


    public void IniciarDialogo()
    {

        if (playerEnRango)
        {
            if (!dialogoEmpezado)
            {
                dialogoEmpezado = true;
                dialogoPanel.SetActive(true);
                textoNombre.text = nombreNPC;
                marcaDialogo.SetActive(false);
                lineaIndex = 0;
                StartCoroutine(MostrarLineas());
            }
            else if (textoDialogo.text == lineasDialogo[lineaIndex])
            {
                SiguienteLinea();
            }
            else
            {
                StopAllCoroutines();
                textoDialogo.text = lineasDialogo[lineaIndex];
            }
        }
    }

    private void SiguienteLinea()
    {
        lineaIndex++;
        if (lineaIndex < lineasDialogo.Length)
        {
            StartCoroutine(MostrarLineas());
        }
        else
        {
            dialogoEmpezado = false;
            dialogoPanel.SetActive(false);
            marcaDialogo.SetActive(true);
        }
    }

    private IEnumerator MostrarLineas()
    {
        textoDialogo.text = string.Empty;
        foreach (char ch in lineasDialogo[lineaIndex])
        {
            textoDialogo.text += ch;
            yield return new WaitForSeconds(tiempoLetras);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            marcaDialogo.SetActive(true);
            //btnDialogo.SetActive(true);
            playerEnRango = true;
            ControlPlayer.Instance.SetCurrentNPC(this); // Añadido para establecer el NPC actual
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            marcaDialogo.SetActive(false);
            // btnDialogo.SetActive(false);
            playerEnRango = false;
            ControlPlayer.Instance.SetCurrentNPC(null); // Añadido para eliminar el NPC actual
        }
    }
}
