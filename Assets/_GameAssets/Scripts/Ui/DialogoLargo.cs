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
    public bool noTieneEvento = true;
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
            var uiManager = gm.GetComponent<UiManager>();
            dialogoPanel = uiManager.dialogoPanel;
            textoNombre = uiManager.textoNombre.GetComponent<TextMeshProUGUI>();
            textoDialogo = uiManager.textoDialogo.GetComponent<TextMeshProUGUI>();
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
            if (!dialogoEmpezado)
            {

                dialogoEmpezado = true;
                dialogoPanel.SetActive(true);
                textoNombre.text = nombreNPC;
                marcaDialogo.SetActive(false);
                lineaIndex = 0;
                ComprobarEvento();
                if (noTieneEvento || !eventoHecho)
                {
                    StartCoroutine(MostrarLineas(lineasDialogo));
                }
                else
                {
                    StartCoroutine(MostrarLineas(lineasDialogo2));
                }
            }
            else
            {
                ContinuarDialogo();
            }
        }
    }

    private void ContinuarDialogo()
    {
        if (noTieneEvento || !eventoHecho)
        {
            ProcesarDialogo(lineasDialogo);
        }
        else
        {
            ProcesarDialogo(lineasDialogo2);
        }
    }

    private void ProcesarDialogo(string[] lineas)
    {
        if (textoDialogo.text == lineas[lineaIndex])
        {
            SiguienteLinea(lineas);
        }
        else
        {
            StopAllCoroutines();
            textoDialogo.text = lineas[lineaIndex];
        }
    }

    private void SiguienteLinea(string[] lineas)
    {
        lineaIndex++;
        if (lineaIndex < lineas.Length)
        {
            StartCoroutine(MostrarTexto(lineas));
        }
        else
        {
            TerminarDialogo();
            if (eventoHecho)
            {
                EjecutarEvento();
            }
        }
    }

    private void TerminarDialogo()
    {

        dialogoEmpezado = false;
        dialogoPanel.SetActive(false);
        marcaDialogo.SetActive(true);

    }
    public void ComprobarEvento()
    {
        espada = gm.GetComponent<EventosManager>().espada;
        if (espada)
        {
            eventoHecho = true;
        }


    }
    private void EjecutarEvento()
    {
        if (espada)
        {
            gm.GetComponent<EventosManager>().Evento(nombreEvento);
            eventoHecho = true;
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
        }
    }

    private IEnumerator MostrarLineas(string[] lineas)
    {
        yield return MostrarTexto(lineas);
    }

    private IEnumerator MostrarTexto(string[] lineas)
    {
        textoDialogo.text = string.Empty;
        foreach (char ch in lineas[lineaIndex])
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
        Instantiate(objetoSpaw, puntoSpawn.position, puntoSpawn.rotation);
    }
}
