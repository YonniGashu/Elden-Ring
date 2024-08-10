using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;
        public PlayerManager player;
        public Camera cameraObject;
        [SerializeField] Transform cameraPivotTransform;

        //Change these to tweak camera performance
        [Header("Camera Settings")]
        private float cameraSmoothSpeed = 1; // The bigger the smooth speed, the longer it will take for the camera to move to the character
        [SerializeField] float upAndDownRotationSpeed = 220;
        [SerializeField] float leftAndRightRotationSpeed = 220;
        [SerializeField] float minimumPivot = -30; //Lowest point you can look down
        [SerializeField] float maximumPivot = 60; //Highest point you can look up
        [SerializeField] float cameraCollisionRadius = 0.2f;
        [SerializeField] LayerMask collideWithLayers;

        [Header("Camera Values")]
        private Vector3 cameraVelocity;
        private Vector3 cameraObjectPosition; //Used for camera collisions (moves the camera object to this position)
        [SerializeField] float leftAndRightLookAngle;
        [SerializeField] float upAndDownLookAngle;
        private float cameraZPosition; //values used for the camera collisions
        private float targetZCameraPosition; //values used for the camera collisions


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            cameraZPosition = cameraObject.transform.localPosition.z;
        }

        public void HandleAllCameraActions()
        {
            if (player != null)
            {
                // Follow the player
                HandleFollowTarget();
                //Rotate around the player
                HandleRotations();
                //Collide with the environment
                HandleCollisions();
            }
        }

        private void HandleFollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPosition;        
        }

        private void HandleRotations()
        {
            //IF we locked on, force rotation to the locked on target
            // else we just rotate normally

            //Normal rotation
            //Rotate left and right based on horizontal input on the mouse
            leftAndRightLookAngle += (PlayerInputManager.instance.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
            //Rotate up and down based on vertical input on the mouse
            upAndDownLookAngle -= (PlayerInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
            //Clamp the up and down between a minimum and maximum value
            upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

            Vector3 cameraRotation = Vector3.zero;
            Quaternion targetRotation;

            //Rotate this gameobject left and right
            cameraRotation = Vector3.zero;
            cameraRotation.y = leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;

            //Rotate the pivot up and down
            cameraRotation = Vector3.zero;
            cameraRotation.x = upAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            cameraPivotTransform.localRotation = targetRotation;
        }

        private void HandleCollisions()
        {
            targetZCameraPosition = cameraZPosition;
            RaycastHit hit;
            //direction for collision
            Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
            direction.Normalize();

            //Check if theres an object infront of our direction
            if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetZCameraPosition), collideWithLayers))
            {
                //If something is there, we get the distance from it
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
                //Then we equate our target Z position to the following
                targetZCameraPosition = -(distanceFromHitObject - cameraCollisionRadius);
            }

            //If our target position is less than our collision radius, we subtract our collision radius (making it snap back)
            if (Mathf.Abs(targetZCameraPosition) < cameraCollisionRadius)
            {
                targetZCameraPosition = -cameraCollisionRadius;
            }

            //then we apply our final position using a lerp over a time of 0.2f
            cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetZCameraPosition, 0.2f);
            cameraObject.transform.localPosition = cameraObjectPosition;
        }
    }
}
