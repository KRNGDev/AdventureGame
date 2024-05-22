using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraUtil
{
    public class FollowCamera : MonoBehaviour
    {
        public string targetTag;
        private Transform target;
        public float height = 12;
        public float distance = 7;
        public float angle = 30f;
        public float followSpeed = 1f;
        public bool rotateCamera = true;

        private Vector3 VelReferencia;

        Vector3 finalPosition;
        Vector3 worldPosition;
        Vector3 flatTargetPosition;
        Vector3 rotatedVector;
        Quaternion playerRotation;
        Vector3 cameraOffset;
       
        void Start()
        {
            print("Start");
            if (GameObject.FindGameObjectsWithTag(targetTag).Length != 1)
            {
                Debug.LogError("No se encuentra el tag del target o hay más de uno");
            }
            else
            {
                target = GameObject.FindGameObjectWithTag(targetTag).transform;
            }
        }

        void Update()
        {
            ManejoCamara();
        }


        protected void ManejoCamara()
        {
            if (!target)
            {
                Debug.LogError("No se encuentra el tag del target o hay más de uno");
                return;
            }
            if (rotateCamera)
            {
                playerRotation = target.rotation;
                cameraOffset = playerRotation * new Vector3(0, -height, -distance);
                finalPosition = target.position + cameraOffset;
                transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref VelReferencia, followSpeed);
                transform.LookAt(target.position + target.forward * 2);
            }
            else
            {
                worldPosition = (Vector3.forward * -distance) + (Vector3.up * height);
                playerRotation = target.rotation;
                rotatedVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPosition;
                flatTargetPosition = target.position;
                finalPosition = flatTargetPosition + rotatedVector;
                transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref VelReferencia, followSpeed);
                transform.LookAt(flatTargetPosition);
            }
        }
    }
}