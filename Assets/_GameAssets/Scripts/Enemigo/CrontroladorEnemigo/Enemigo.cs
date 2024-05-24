

using System.Collections;
using System.Collections.Generic;
using Enemigo;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

namespace enemigo
{
    public class Enemigo : MonoBehaviour
    {
        [Header("Objetivo a seguir")]
        public float damage = 2;
        public float rangoDeteccion = 10.0f;
        public string nombreTagAtacar = "Player";
        [Header("Objetivo a seguir")]
        public bool seguir;
        public string nombreTagObjetivo;
        public float distanciaAtaque = 1.5f;
        public float velDisparo = 10;
        public float tiempoMagia = 2;
        public GameObject bolaFuegoPrefab;
        public Transform spawnSlash;
        [Header("Estados Enemigo")]
        public bool atacando = false;

        private Animator animator;
        private GameObject target;
        private GameObject targetSeguir;

        private bool muerto;
        public bool congelado = false;
        private NavMeshAgent navMeshAgent;
        private bool esperandoEnemigo = false;
        private float x, y;
        public float tiempoEspera = 0.5f;
        public List<Transform> targets;
        private int nextTarget = 0;

        void Start()
        {
            ConstraintSource cs = new ConstraintSource();
            cs.weight = 1;
            cs.sourceTransform = Camera.main.transform;
            GetComponentInChildren<LookAtConstraint>().AddSource(cs);
            animator = GetComponent<Animator>();
            target = GameObject.FindWithTag(nombreTagAtacar);

            if (nombreTagObjetivo != "")
            {
                targetSeguir = GameObject.FindWithTag(nombreTagObjetivo);
            }

            // Inicializar NavMeshAgent
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {

            if (!atacando)
            {
                x = 0;
                y = navMeshAgent.velocity.magnitude;
                muerto = GetComponent<SistemaVidaEnemigo>().muerto;



                animator.SetFloat("X", x);
                animator.SetFloat("Y", y);
                BuscarEnemigos();
                targets.RemoveAll(item => item == null || item.gameObject == null);
                if (seguir)
                {
                    // GetComponent<PatrullajeManager>().enabled = false;
                    SeguirJugador();

                    // Limpiar la lista de targets eliminados o fuera del rango de detección
                    targets.RemoveAll(item => item == null || Vector3.Distance(transform.position, item.position) > rangoDeteccion);

                    // Asignar el primer target de la lista
                    if (targets.Count > 0)
                    {
                        target = targets[0].gameObject;
                    }
                    else
                    {
                        target = null;
                    }

                    // Buscar enemigos si no hay un target válido
                    if (target == null)
                    {
                        BuscarEnemigos();
                    }

                    // Comprobar si el target es null y asignar el siguiente en la lista
                    if (target == null && targets.Count > 0)
                    {
                        AsignarSiguienteTargetSecuencial();
                    }

                    // Mover y atacar al target
                    if (target != null && Vector3.Distance(transform.position, target.transform.position) < rangoDeteccion)
                    {
                        navMeshAgent.destination = target.transform.position;
                        if (Vector3.Distance(transform.position, target.transform.position) < distanciaAtaque)
                        {
                            transform.LookAt(target.transform);
                            animator.SetBool("ataca", true);
                        }
                        else
                        {
                            animator.SetBool("ataca", false);
                        }
                    }
                    else if (seguir && targetSeguir != null)
                    {
                        SeguirJugador();
                        navMeshAgent.isStopped = false; // Asegura que el agente no esté detenido
                    }
                }
                else
                {

                    ComportamientoEnemigo();
                }
            }
        }

        private void ComportamientoEnemigo()
        {
            GetComponent<PatrullajeManager>().enabled = true;
            if (target != null && Vector3.Distance(transform.position, target.transform.position) < rangoDeteccion)
            {
                navMeshAgent.destination = target.transform.position;
                if (Vector3.Distance(transform.position, target.transform.position) < distanciaAtaque)
                {
                    transform.LookAt(target.transform);
                    animator.SetBool("ataca", true);
                }
                else
                {
                    animator.SetBool("ataca", false);
                }
            }
        }

        private void SeguirJugador()
        {
            if (targetSeguir != null && Vector3.Distance(transform.position, targetSeguir.transform.position) > 3.5f)
            {
                navMeshAgent.destination = targetSeguir.transform.position;
                navMeshAgent.isStopped = false; // Asegura que el agente no esté detenido
            }
            else if (targetSeguir != null)
            {
                navMeshAgent.isStopped = true; // Detiene el agente
                transform.LookAt(targetSeguir.transform);
            }
        }

        private void BuscarEnemigos()
        {
            // Buscar enemigos en el rango de detección
            Collider[] colliders = Physics.OverlapSphere(transform.position, rangoDeteccion);

            // Limpiar la lista de objetivos antes de añadir los nuevos detectados
            targets.Clear();

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag(nombreTagAtacar))
                {
                    // Añadir el transform del enemigo a la lista de objetivos
                    targets.Add(collider.transform);


                }
            }
        }

        private void AsignarSiguienteTargetSecuencial()
        {
            /*esperandoEnemigo = false;
            nextTarget++;
            if (nextTarget == targets.Count) nextTarget = 0;
            target = targets[nextTarget].gameObject;
            navMeshAgent.destination = targets[nextTarget].position;*/
            if (targets.Count > 0)
            {
                targets.RemoveAt(0); // Eliminar el target actual de la lista
                targets.RemoveAll(item => item == null); // Limpiar la lista de targets eliminados
                if (targets.Count > 0)
                {
                    target = targets[0].gameObject; // Asignar el siguiente target en la lista
                }
                else
                {
                    target = null;
                }
            }
        }

        public void Atacar()
        {
            if (target != null && Vector3.Distance(transform.position, target.transform.position) < distanciaAtaque)
            {
                transform.LookAt(target.transform);
                animator.SetBool("ataca", true);
            }
            else
            {
                animator.SetBool("ataca", false);
            }
        }

        public void Detenerse()
        {
            atacando = true;
        }

        public void BolaFuego()
        {
            GameObject bolaFuego = Instantiate(bolaFuegoPrefab, spawnSlash.position, spawnSlash.rotation);
            bolaFuego.GetComponent<Rigidbody>().velocity = spawnSlash.forward * velDisparo;
            StartCoroutine(TiempoMagia(tiempoMagia));
        }

        public void Moverse()
        {
            atacando = false;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, distanciaAtaque);
        }

        IEnumerator TiempoMagia(float tiempo)
        {
            yield return new WaitForSeconds(tiempo);
            Moverse();
        }
    }
}



/*using System.Collections;using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
namespace enemigo
{
    public class Enemigo : MonoBehaviour
    {
        [Header("Objetivo a seguir")]
        public float rangoDeteccion = 10.0f;
        public string nombreTagAtacar = "Player";
        [Header("Objetivo a seguir")]
        public bool seguir;
        public string nombreTagObjetivo;
        public float distanciaAtaque = 1.5f;
        public float velDisparo = 10;
        public float tiempoMagia = 2;
        public GameObject bolaFuegoPrefab;
        public Transform spawnSlash;
        [Header("Estados Enemigo")]
        public bool atacando = false;

        private Animator animator;
        private GameObject target;
        private GameObject targetSeguir;

        private bool muerto;
        public bool congelado = false;
        private NavMeshAgent navMeshAgent;


        // Start is called before the first frame update
        void Start()
        {
            ConstraintSource cs = new ConstraintSource();
            cs.weight = 1;
            cs.sourceTransform = Camera.main.transform;
            GetComponentInChildren<LookAtConstraint>().AddSource(cs);
            animator = GetComponent<Animator>();
            target = GameObject.FindWithTag(nombreTagAtacar);

            if (nombreTagObjetivo != "")
            {
                targetSeguir = GameObject.FindWithTag(nombreTagObjetivo);
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (!atacando)
            {
                if (seguir)
                {
                    if (Vector3.Distance(transform.position, targetSeguir.transform.position) > 3.5)
                    {
                        GetComponent<NavMeshAgent>().destination = targetSeguir.transform.position;
                        GetComponent<NavMeshAgent>().isStopped = false; // Asegura que el agente no esté detenido
                    }
                    else
                    {
                        GetComponent<NavMeshAgent>().isStopped = true; // Detiene el agente
                        transform.LookAt(targetSeguir.transform);
                    }
                    /* if (Vector3.Distance(transform.position, target.transform.position) < rangoDeteccion)
                    {
                        GetComponent<NavMeshAgent>().destination = target.transform.position;
                        if (Vector3.Distance(transform.position, target.transform.position) < distanciaAtaque)
                        {
                            transform.LookAt(target.transform);
                            animator.SetBool("ataca", true);
                            Detenerse();
                        }
                        else
                        {
                            animator.SetBool("ataca", false);
                        }
                    }*//*
                    Collider[] colliders = Physics.OverlapSphere(transform.position, rangoDeteccion);

                    // Recorrer los colliders y buscar uno con el tag "Enemigo"
                    foreach (Collider collider in colliders)
                    {print("Buscando");
                        if (collider.CompareTag(nombreTagAtacar))
                        print(nombreTagAtacar);
                        {print(" enemigo  ");
                            // Si se encuentra un enemigo, establecer su posición como destino
                            navMeshAgent.destination = collider.transform.position;
                            navMeshAgent.isStopped = false; // Asegura que el agente no esté detenido
                            return; // Salir del loop una vez que se encuentra un enemigo
                        }
                    }




                }
                else
                {
                    if (Vector3.Distance(transform.position, target.transform.position) < rangoDeteccion)
                    {
                        GetComponent<NavMeshAgent>().destination = target.transform.position;
                        if (Vector3.Distance(transform.position, target.transform.position) < distanciaAtaque)
                        {
                            transform.LookAt(target.transform);
                            animator.SetBool("ataca", true);
                        }
                        else
                        {
                            animator.SetBool("ataca", false);
                        }
                    }
                }
            }

        }

        public void Atacar()
        {print("ataca");

            if (Vector3.Distance(transform.position, target.transform.position) < distanciaAtaque)
            {
                transform.LookAt(target.transform);
                animator.SetBool("ataca", true);
            }
            else
            {
                animator.SetBool("ataca", false);
            }
        }
        public void Detenerse()
        {
            atacando = true;
        }
        public void BolaFuego()
        {
            GameObject bolaFuego = Instantiate(bolaFuegoPrefab, spawnSlash.position, spawnSlash.rotation);
            bolaFuego.GetComponent<Rigidbody>().velocity = spawnSlash.forward * velDisparo;
            StartCoroutine(TiempoMagia(tiempoMagia));
        }

        public void Moverse()
        {
            atacando = false;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, distanciaAtaque);
        }

        IEnumerator TiempoMagia(float tiempo)
        {
            print("espera");
            yield return new WaitForSeconds(tiempo);
            print("esperado");
            Moverse();
        }



    }
}*/