using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CargarEscena : MonoBehaviour
{
    public string sceneLoadName;
    public TextMeshProUGUI textProgress;
    public Slider sliderProgress;
    public float currentPercent;
    private AsyncOperation loadAsync;
    public GameObject btnContinuar;
    void Awake()
    {
        StopAllCoroutines();
    }
    void Start()
    {

        sceneLoadName = GameObject.Find("EsceneManager").GetComponent<EsceneManager>().nombreEscena;

        Debug.Log("Entra cargar" + sceneLoadName);
        StartCoroutine(LoadEscena(sceneLoadName));
    }
    public IEnumerator LoadEscena(string nombreACargar)
    {
        Debug.Log("a ver si carga" + nombreACargar);
        textProgress.text = "Cargando.. 00%";
        loadAsync = SceneManager.LoadSceneAsync(nombreACargar);
        loadAsync.allowSceneActivation = false;
        while (!loadAsync.isDone)
        {
            currentPercent = loadAsync.progress * 100 / 0.9f;
            textProgress.text = "Cargando.. " + sliderProgress.value.ToString("00") + "%";
            yield return null;
        }
    }


    // Update is called once per frame
    void Update()
    {
        sliderProgress.value = Mathf.MoveTowards(sliderProgress.value, currentPercent, 20 * Time.deltaTime);
        if (loadAsync != null && sliderProgress.value >= 100)
        {
            btnContinuar.SetActive(true);
        }
    }
    public void Continuar()
    {
        loadAsync.allowSceneActivation = true;
    }
}
