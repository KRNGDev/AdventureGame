using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{

    public static MainManager Instance;
    private static string KEY_VOLUME = "Volumen";
    public static string KEY_NAME = "Nombre";
    public TextMeshProUGUI inputName;
    public TextMeshProUGUI textNombre;
    public AudioSource audiosourceBSO;
    public Slider slider;
    public Button boton;
    private GameObject panelOpciones;

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
    private void Start()
    {
        slider = GameObject.Find("SliderVolumen").GetComponent<Slider>();
        RestoreVolume();


        panelOpciones = GameObject.Find("PanelOpciones");
        panelOpciones.SetActive(false);
    }
    void Update()
    {
        if (!slider)
        {
            slider = GameObject.Find("SliderVolumen").GetComponent<Slider>();
            slider.onValueChanged.AddListener(delegate { ModificarVolumen(); });

            RestoreVolume();

        }



        if (!panelOpciones)
        {
            panelOpciones = GameObject.Find("PanelOpciones");
            panelOpciones.SetActive(false);
            print("Opciones");
            GameObject gm = GameObject.Find("GameManager");
            gm.GetComponent<UiManager>().CerrarPanel("menu");
        }

    }

    public void RestoreVolume()
    {
        if (PlayerPrefs.HasKey(KEY_VOLUME))
        {

            slider.value = PlayerPrefs.GetFloat("Volumen");

        }
        else
        {
            slider.maxValue = audiosourceBSO.volume;
            slider.value = slider.maxValue;
        }
    }
    public void SaveAudioVolume()
    {
        PlayerPrefs.SetFloat(KEY_VOLUME, slider.value);
        PlayerPrefs.Save();
        print("Guardado ");
    }
    public void ModificarVolumen()
    {
        if (slider == null)
        {
            slider = GameObject.Find("SliderVolumen").GetComponent<Slider>();
        }

        audiosourceBSO.volume = slider.value;
    }
    public void SaveName()
    {
        inputName=GameObject.Find("Nombre").GetComponent<TextMeshProUGUI>();
        print(inputName.text);
        PlayerPrefs.SetString(KEY_NAME, inputName.text);
        PlayerPrefs.Save();
        
    }
    public void RestoreName()
    {
        if (PlayerPrefs.HasKey(KEY_NAME))
        {
            textNombre=GameObject.Find("TextoNombre").GetComponent<TextMeshProUGUI>();

            textNombre.text = PlayerPrefs.GetString("Nombre");

        }
    }
}
