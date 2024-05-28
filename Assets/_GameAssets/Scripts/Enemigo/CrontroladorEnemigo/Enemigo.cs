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
        public bool seguir;
        public string nombreTagObjetivo;
        public float distanciaAtaque = 1.5f;
        public float velDisparo = 10;
        public float tiempoMagia = 2;
        public GameObject bolaFuegoPrefab;
        public Transform spawnSlash;
        
        [Header("Estados Enemigo")]
        public bool atacando = false;
        public bool congelado = false;

        private Animator animator;
        private GameObject target;
        private GameObject targetSeguir;
        private bool muerto;
        private bool esperandoEnemigo = false;
        private float x, y;
        private float tiempoEspera = 0.5f;
        private NavMeshAgent navMeshAgent;
        private List<Transform> targets = new List<Transform>();
        private int nextTarget = 0;

        void Start()
        {
            ConstraintSource cs = new ConstraintSource
            {
                weight = 1,
                sourceTransform = Camera.main.transform
            };
            GetComponentInChildren<LookAtConstraint>().AddSource(cs);

            animator = GetComponent<Animator>();
            target = GameObject.FindWithTag(nombreTagAtacar);

            if (!string.IsNullOrEmpty(nombreTagObjetivo))
            {
                targetSeguir = GameObject.FindWithTag(nombreTagObjetivo);
            }

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

                targets.RemoveAll(item => item == null || item.gameObject == null);

                if (seguir)
                {
                    SeguirJugador();

                    targets.RemoveAll(item => item == null || Vector3.Distance(transform.position, item.position) > rangoDeteccion);

                    if (targets.Count > 0)
                    {
                        target = targets[0].gameObject;
                    }
                    else
                    {
                        target = null;
                    }

                    if (target == null)
                    {
                        BuscarEnemigos();
                    }

                    if (target == null && targets.Count > 0)
                    {
                        AsignarSiguienteTargetSecuencial();
                    }

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
                        navMeshAgent.isStopped = false;
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
            if (targetSeguir != null)
            {
                if (Vector3.Distance(transform.position, targetSeguir.transform.position) > 3.5f)
                {
                    navMeshAgent.destination = targetSeguir.transform.position;
                    navMeshAgent.isStopped = false;
                }
                else
                {
                    navMeshAgent.isStopped = true;
                    transform.LookAt(targetSeguir.transform);
                }
            }
        }

        private void BuscarEnemigos()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, rangoDeteccion);
            targets.Clear();

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag(nombreTagAtacar))
                {
                    targets.Add(collider.transform);
                }
            }
        }

        private void AsignarSiguienteTargetSecuencial()
        {
            if (targets.Count > 0)
            {
                targets.RemoveAt(0);
                targets.RemoveAll(item => item == null);

                if (targets.Count > 0)
                {
                    target = targets[0].gameObject;
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

        private IEnumerator TiempoMagia(float tiempo)
        {
            yield return new WaitForSeconds(tiempo);
            Moverse();
        }
    }
}
