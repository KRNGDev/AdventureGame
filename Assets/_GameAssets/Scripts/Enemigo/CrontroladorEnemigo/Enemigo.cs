using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
namespace enemigo
{
    public class Enemigo : MonoBehaviour
    {


        [Header("Estados Enemigo")]
        public bool atacando = false;
        [Header("GameObject Player")]
        private Animator animator;
        private GameObject target;
        private bool muerto;
        public bool congelado = false;


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

            if (Vector3.Distance(transform.position, target.transform.position) < 8)
            {


                GetComponent<NavMeshAgent>().destination = target.transform.position;
                if (Vector3.Distance(transform.position, target.transform.position) < 2)
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
