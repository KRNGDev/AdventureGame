using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class TopDownCamara : MonoBehaviour
    {
        #region Variables
        public Transform m_target;
        public float m_Altura = 16;
        public float m_Distancia = 5;
        public float m_Angulo = -5;
        public float m_SmoothSpeed = 1f;
        public string aquienSeguir = "";

        public bool canvas = false;
        //public ProyectoTank.MenuInicio.MenuPrincipal canvas;

        public Vector3 VelReferencia;


        #endregion



        #region Main Metodos
        // Start is called before the first frame update
        void Start()
        {

            StartCoroutine("Tiempo");
            BuscarTarget(aquienSeguir);

        }

        public void BuscarTarget(string nombre)
        {
            m_target = GameObject.Find(nombre).GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            if (canvas)
            {
                ManejoCamara();

            }

        }
        #endregion


        #region Metodos auxiliares
        protected void ManejoCamara()
        {
            if (!m_target)
            {
                return;
            }
            //Construir el vector de posicion Mundial
            Vector3 worldPosition = (Vector3.forward * -m_Distancia) + (Vector3.up * m_Altura);
            //Debug.DrawLine(m_target.position, worldPosition, Color.red);

            //Construir nuestro vector de rotacion
            Quaternion playerRotation = m_target.rotation;
            float playerAngle = Quaternion.Angle(Quaternion.identity, playerRotation);

            Vector3 rotatedVector = Quaternion.AngleAxis(m_Angulo, Vector3.up) * worldPosition;
            // Debug.DrawLine(m_target.position, rotatedVector, Color.green);

            //Mover Nuestra posicion
            Vector3 flatTargetPosition = m_target.position;

            //flatTargetPosition.y = 0f;
            Vector3 finalPosition = flatTargetPosition + rotatedVector;
            //Debug.DrawLine(m_target.position, finalPosition, Color.blue);

            transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref VelReferencia, m_SmoothSpeed);


            transform.LookAt(flatTargetPosition);

            /*// Obtenemos la rotación actual del jugador
            Quaternion playerRotation = m_target.rotation;

            // Rotamos la posición de la cámara respecto a la rotación del jugador
            Vector3 cameraOffset = playerRotation * new Vector3(0, -m_Altura, -m_Distancia);

            // Calculamos la posición final de la cámara
            Vector3 finalPosition = m_target.position + cameraOffset;

            // Actualizamos la posición de la cámara suavemente
            transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref VelReferencia, m_SmoothSpeed);

            // Hacemos que la cámara mire hacia donde mira el jugador
            transform.LookAt(m_target.position + m_target.forward * 2); // Ajusta el multiplicador según tu escala de mundo*/



        }
        IEnumerator Tiempo()
        {


            yield return new WaitForSeconds(1.5F);


            //canvas = GameObject.FindGameObjectWithTag("Panel Opciones").GetComponent<ProyectoTank.MenuInicio.MenuPrincipal>();
            ManejoCamara();
            canvas = true;
            //canvas.SetActive(true);

        }
        #endregion
    }
}
