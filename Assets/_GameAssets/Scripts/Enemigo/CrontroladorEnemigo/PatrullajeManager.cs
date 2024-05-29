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
        private float tiempoEspera;
        private int nextTarget = 0;
        private NavMeshAgent navMeshAgent;
        private Animator animator;
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

            if (navMeshAgent.isOnNavMesh && navMeshAgent.isActiveAndEnabled)
            {
                navMeshAgent.destination = targets[nextTarget].position;
            }
            else
            {
                Debug.LogWarning("NavMeshAgent no está en el NavMesh al iniciar.");
            }
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
            if (esperandoAsignacion || navMeshAgent == null || !navMeshAgent.isOnNavMesh || !navMeshAgent.isActiveAndEnabled)
                return;

            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    tiempoEspera = Random.Range(0, tiempoMax);
                    esperandoAsignacion = true;

                    if (patrullajeAleatorio)
                    {
                        Invoke(nameof(AsignarSiguienteTargetAleatorio), tiempoEspera);
                    }
                    else
                    {
                        Invoke(nameof(AsignarSiguienteTargetSecuencial), tiempoEspera);
                    }
                }
            }
        }

        private void AsignarSiguienteTargetAleatorio()
        {
            esperandoAsignacion = false;
            nextTarget = Random.Range(0, targets.Count);

            if (!siguiendo && navMeshAgent.isOnNavMesh && navMeshAgent.isActiveAndEnabled)
            {
                navMeshAgent.destination = targets[nextTarget].position;
            }
            else
            {
                Debug.LogWarning("NavMeshAgent no está activo o no está en el NavMesh.");
            }
        }

        private void AsignarSiguienteTargetSecuencial()
        {
            esperandoAsignacion = false;
            nextTarget = (nextTarget + 1) % targets.Count;

            if (navMeshAgent.isOnNavMesh && navMeshAgent.isActiveAndEnabled)
            {
                navMeshAgent.destination = targets[nextTarget].position;
            }
            else
            {
                Debug.LogWarning("NavMeshAgent no está activo o no está en el NavMesh.");
            }
        }
    }
}
