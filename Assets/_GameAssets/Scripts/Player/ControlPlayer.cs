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
    public Joystick joystick;
    private Rigidbody rbPlayer;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        camara = GameObject.Find("MainCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
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
            if (atacando == false)
            {
                x = joystick.Horizontal;
                y = joystick.Vertical;

                animator.SetFloat("X", y);
                animator.SetFloat("Y", x);
                /* transform.Rotate(0, x * Time.deltaTime * velRotacion, 0);
                 transform.Translate(0, 0, y * Time.deltaTime * velMovimiento);*/

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
        /* else
         {
             if (atacando == false && defendiendo == false)
             {
                 x = Input.GetAxis("Horizontal");
                 y = Input.GetAxis("Vertical");

                 transform.Rotate(0, x * Time.deltaTime * velRotacion, 0);
                 transform.Translate(0, 0, y * Time.deltaTime * velMovimiento);

                 animator.SetFloat("X", x);
                 animator.SetFloat("Y", y);
                 if (Input.GetButtonDown("Jump") && estaEnSuelo)
                 {

                     //rbPlayer.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
                     animator.SetBool("Saltando", true);
                     animator.SetBool("EstaEnSuelo", false);
                     estaEnSuelo = false;


                 }
             }
         }*/

    }
    /*public void Salto()
    {
        if (estaEnSuelo)
        {
            animator.SetBool("Saltando", true);
            animator.SetBool("EstaEnSuelo", false);
            rbPlayer.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            estaEnSuelo = false;
        }
    }*/
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
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("suelo"))
        {
            animator.SetBool("EstaEnSuelo", false);
            estaEnSuelo = false;
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

    /* public void OnCollisionEnter(Collision other)
     {
         if (!estaEnSuelo)
         {
             ContactPoint cp = other.GetContact(0);
             Vector3 direccion = cp.normal;
             GetComponentInChildren<Rigidbody>().AddForce(direccion * 150);
             GetComponent<ControlPlayer>().enabled = false;
             GetComponent<Animator>().applyRootMotion = false;
         }

     }*/

}
