using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
namespace enemigo
{
    public class EnemigoKamikace : MonoBehaviour
    {

        [Header("Datos Enemigo")]
        public int puntosDano;
        public float tiempoCongelado = 1;
        public float velMovimiento = 5.0f;
        public float distanciaAtaque;
        public GameObject rayos;

        [Header("Estados Enemigo")]
        public bool atacando = false;
        public bool defendiendo = false;
        public bool congelado = false;
        public bool estaEnSuelo = true;


        [Header("GameObject Player")]
        private Animator animator;

        private GameObject target;
        private bool muerto = false;




        // Start is called before the first frame update
        void Start()
        {
            ConstraintSource cs = new ConstraintSource();
            cs.weight = 1;
            cs.sourceTransform = Camera.main.transform;
            GetComponentInChildren<LookAtConstraint>().AddSource(cs);

            animator = GetComponent<Animator>();
            target = GameObject.FindGameObjectWithTag("Player");

        }

        // Update is called once per frame
        void Update()
        {
            muerto = GetComponent<SistemaVidaEnemigo>().muerto;
            if (!muerto)
            {
                if (!atacando)
                {
                    animator.SetFloat("Y", 1);
                    transform.Translate(Vector3.forward * velMovimiento * Time.deltaTime);
                    transform.LookAt(target.transform.position);
                }

                if (Vector3.Distance(transform.position, target.transform.position) < distanciaAtaque)
                {
                    atacando = true;


                    animator.SetBool("ataca", true);

                    //transform.LookAt(target.transform);
                }
                else
                {
                    atacando = false;
                    animator.SetBool("ataca", false);
                }

            }
            if (congelado)
            {
                Animacion();
            }

        }

        public void Animacion()
        {
            congelado = false;
            GetComponent<Animator>().enabled = false;
            GameObject aturdido = Instantiate(rayos, transform.position, transform.rotation, transform);
            aturdido.transform.parent = transform;
            Invoke("Activar", tiempoCongelado);
        }
        public void Activar()
        {
            GetComponent<Animator>().enabled = true;

        }


    }
}
