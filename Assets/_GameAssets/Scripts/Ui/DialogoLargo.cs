using UnityEngine;
using TMPro;
using System.Collections;

public class DialogoLargo : MonoBehaviour
{
    [Header("Datos Dialogo")]
    [SerializeField] private GameObject marcaDialogo;
    private GameObject dialogoPanel;
    private TextMeshProUGUI textoDialogo;
    private TextMeshProUGUI textoNombre;
    public GameObject objetoSpaw;
    public Transform puntoSpawn;
    public bool espada;
    public bool puerta;
    public bool eventoHecho;
    public string nombreEvento;
    public float tiempoLetras;
    public bool playerEnRango;
    public bool dialogoEmpezado;

    public int lineaIndex;
    [Header("Texto para NPC")]
    public string nombreNPC;
    [SerializeField, TextArea(4, 6)] private string[] lineasDialogo;
    [SerializeField, TextArea(4, 6)] private string[] lineasDialogo2;
    private GameObject gm;

    void Start()
    {
        gm = GameObject.Find("GameManager");
        if (gm != null)
        {
            dialogoPanel = gm.GetComponent<UiManager>().dialogoPanel;
            textoNombre = gm.GetComponent<UiManager>().textoNombre.GetComponent<TextMeshProUGUI>();
            textoDialogo = gm.GetComponent<UiManager>().textoDialogo.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("No se pudo encontrar el GameManager.");
        }
    }

    public void IniciarDialogo()
    {
        if (playerEnRango)
        {
            if (!eventoHecho)
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
            else
            {
                if (!dialogoEmpezado)
                {
                    dialogoEmpezado = true;
                    dialogoPanel.SetActive(true);
                    textoNombre.text = nombreNPC;
                    marcaDialogo.SetActive(false);
                    lineaIndex = 0;
                    StartCoroutine(MostrarLineas2());
                }
                else if (textoDialogo.text == lineasDialogo2[lineaIndex])
                {
                    SiguienteLinea();
                }
                else
                {
                    StopAllCoroutines();
                    textoDialogo.text = lineasDialogo2[lineaIndex];
                }
            }
        }
    }

    private void SiguienteLinea()
    {
        espada = gm.GetComponent<EventosManager>().espada;
        lineaIndex++;
        if (lineaIndex < lineasDialogo.Length)
        {
            if (espada)
            {
                eventoHecho = true;
            }
            if (!eventoHecho)
            {
                StartCoroutine(MostrarLineas());
            }
            else
            {
                StartCoroutine(MostrarLineas2());
            }
        }
        else
        {
            dialogoEmpezado = false;
            dialogoPanel.SetActive(false);
            marcaDialogo.SetActive(true);
            if (espada)
            {
                gm = GameObject.Find("GameManager");
                gm.GetComponent<EventosManager>().Evento(nombreEvento);
            }
            if (nombreEvento == "espada" && gm.GetComponent<GameManager>().puntosActuales > 0)
            {
                if (!eventoHecho)
                {
                    gm.GetComponent<GameManager>().QuitarPuntos();
                    SpawnObjeto();
                    eventoHecho = true;
                    IniciarDialogo();
                }
                return;
            }
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

    private IEnumerator MostrarLineas2()
    {
        textoDialogo.text = string.Empty;
        if (lineaIndex < lineasDialogo2.Length)
        {
            foreach (char ch in lineasDialogo2[lineaIndex])
            {
                textoDialogo.text += ch;
                yield return new WaitForSeconds(tiempoLetras);
            }
        }
        else
        {
            dialogoEmpezado = false;
            dialogoPanel.SetActive(false);
            marcaDialogo.SetActive(true);
            Debug.LogWarning("El índice de línea está fuera del rango para lineasDialogo2.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            marcaDialogo.SetActive(true);
            playerEnRango = true;
            ControlPlayer.Instance.SetCurrentNPC(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            marcaDialogo.SetActive(false);
            playerEnRango = false;
            ControlPlayer.Instance.SetCurrentNPC(null);
        }
    }

    public void SpawnObjeto()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().condicion = true;
        Instantiate(objetoSpaw, puntoSpawn);
    }
}
