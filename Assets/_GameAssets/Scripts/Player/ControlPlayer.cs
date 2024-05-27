using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    public static ControlPlayer Instance;
    private float x, y;
    public bool atacando = false;
    private bool defendiendo;
    private bool estaEnSuelo;
    private Animator animator;
    private Camera camara;
    [Header("Datos Player")]
    public float velRotacion = 200;
    public float velMovimiento = 5;
    public float fuerzaSalto = 10.0f;

    [Header("GameObject Player")]

    [SerializeField] private GameObject btnDialogo;
    public Joystick joystick;
    private Rigidbody rbPlayer;
    private DialogoLargo currentNPC; // Modificado para manejar el NPC más cercano

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        camara = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (!joystick)
        {
            joystick = GameObject.Find("Floating Joystick").GetComponent<Joystick>();
        }
        if (!camara)
        {
            camara = GameObject.Find("MainCamera").GetComponent<Camera>();
        }

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (!atacando)
            {
                x = joystick.Horizontal;
                y = joystick.Vertical;

                animator.SetFloat("X", y);
                animator.SetFloat("Y", x);

                var targetVector = new Vector3(x, 0, y);
                var velocidad = velMovimiento * Time.deltaTime;
                targetVector = Quaternion.Euler(0, camara.gameObject.transform.eulerAngles.y, 0) * targetVector;

                var targetPosition = transform.position + targetVector * velocidad;
                transform.position = targetPosition;

                var movementVector = targetVector;
                if (movementVector.magnitude == 0) { return; }
                var rotation = Quaternion.LookRotation(movementVector);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, velRotacion);
            }
        }
    }

    public void SetCurrentNPC(DialogoLargo npc)
    {
        currentNPC = npc;
    }

    public void TryStartDialogWithNPC()
    {
        if (currentNPC != null)
        {
            print("parece que va");
            currentNPC.IniciarDialogo();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("suelo"))
        {
            animator.SetBool("EstaEnSuelo", true);
            animator.SetBool("Saltando", false);
            estaEnSuelo = true;
            GetComponent<ControlPlayer>().enabled = true;
            GetComponent<Animator>().applyRootMotion = true;
        }
        if (other.CompareTag("Puntos"))
        {
            print("moneda1");
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            gm.SumarPuntos();
            print("moneda");
        }
        if (other.gameObject.CompareTag("NPC"))
        {
            btnDialogo.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("suelo"))
        {
            animator.SetBool("EstaEnSuelo", false);
            estaEnSuelo = false;
        }
        if (other.gameObject.CompareTag("NPC"))
        {
            btnDialogo.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("suelo"))
        {
            estaEnSuelo = true;
            animator.SetBool("EstaEnSuelo", true);
            animator.SetBool("Saltando", false);

            GetComponent<ControlPlayer>().enabled = true;
            GetComponent<Animator>().applyRootMotion = true;
        }
    }
}
