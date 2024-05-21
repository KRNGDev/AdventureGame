using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Enemigo
{
    public class PatrullajeManager : MonoBehaviour
    {
        public List<Transform> targets;
        public bool patrullajeAleatorio = true;

        [Range(1, 10)]
        public float tiempoMax;
        public float tiempoEspera;
        private int nextTarget = 0;
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private float x, y;
        private bool muerto = false;
        private bool siguiendo;

        private bool esperandoAsignacion = false;
        void Start()
        {
            siguiendo = GetComponent<enemigo.Enemigo>().seguir;
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            if (patrullajeAleatorio)
            {
                nextTarget = Random.Range(0, targets.Count);
            }
            navMeshAgent.destination = targets[nextTarget].position;
        }
        void Update()
        {

            if (!siguiendo)
            {
                DeterminarSiguienteTarget();
            }

        }
        private void DeterminarSiguienteTarget()
        {


            if (esperandoAsignacion) return;
            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        if (patrullajeAleatorio)
                        {
                            tiempoEspera = Random.Range(0, tiempoMax);
                            esperandoAsignacion = true;
                            Invoke("AsignarSiguienteTargetAleatorio", tiempoEspera);
                        }
                        else
                        {
                            tiempoEspera = Random.Range(0, tiempoMax);
                            esperandoAsignacion = true;
                            Invoke("AsignarSiguienteTargetSecuencial", tiempoEspera);
                        }
                    }
                }
            }

        }
        private void AsignarSiguienteTargetAleatorio()
        {
            print("patrullaje activo");
            esperandoAsignacion = false;
            nextTarget = Random.Range(0, targets.Count);
            if (!siguiendo)
            {
                navMeshAgent.destination = targets[nextTarget].position;
            }
        }
        private void AsignarSiguienteTargetSecuencial()
        {
            esperandoAsignacion = false;
            nextTarget++;
            if (nextTarget == targets.Count) nextTarget = 0;
            navMeshAgent.destination = targets[nextTarget].position;
        }
    }
}