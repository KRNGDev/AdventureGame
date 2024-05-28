using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    private static string KEY_VOLUME = "Volumen";
    public static string KEY_NAME = "Nombre";

    [Header("UI Components")]
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

        if (PlayerPrefs.HasKey(KEY_NAME))
        {
            RestoreName();
        }
    }

    private void Start()
    {
        InitializeSlider();
        InitializePanelOpciones();
    }

    private void Update()
    {
        if (!slider)
        {
            InitializeSlider();
        }

        if (!panelOpciones)
        {
            InitializePanelOpciones();
        }
    }

    private void InitializeSlider()
    {
        GameObject sliderObject = GameObject.Find("SliderVolumen");
        if (sliderObject != null)
        {
            slider = sliderObject.GetComponent<Slider>();
            if (slider != null)
            {
                slider.onValueChanged.AddListener(delegate { ModificarVolumen(); });
                slider.maxValue = 1;
                RestoreVolume();
            }
            else
            {
                Debug.LogWarning("El objeto encontrado no tiene un componente Slider.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el objeto SliderVolumen.");
        }
    }

    private void InitializePanelOpciones()
    {
        panelOpciones = GameObject.Find("PanelOpciones");
        if (panelOpciones != null)
        {
            panelOpciones.SetActive(false);

            GameObject gm = GameObject.Find("GameManager");
            if (gm != null)
            {
                UiManager uiManager = gm.GetComponent<UiManager>();
                if (uiManager != null)
                {
                    uiManager.CerrarPanel("menu");
                }
            }
        }
    }

    public void RestoreVolume()
    {
        if (PlayerPrefs.HasKey(KEY_VOLUME))
        {
            slider.value = PlayerPrefs.GetFloat(KEY_VOLUME);
        }
        else
        {
            slider.maxValue = 1;
            slider.value = slider.maxValue;
        }
    }

    public void SaveAudioVolume()
    {
        PlayerPrefs.SetFloat(KEY_VOLUME, slider.value);
        PlayerPrefs.Save();
        Debug.Log("Volume saved");
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
        inputName = GameObject.Find("Nombre").GetComponent<TextMeshProUGUI>();
        PlayerPrefs.SetString(KEY_NAME, inputName.text);
        PlayerPrefs.Save();
        Debug.Log("Name saved: " + inputName.text);
    }

    public void RestoreName()
    {
        if (PlayerPrefs.HasKey(KEY_NAME))
        {
            textNombre = GameObject.Find("TextoNombre").GetComponent<TextMeshProUGUI>();
            textNombre.text = PlayerPrefs.GetString(KEY_NAME);
        }
    }
}
