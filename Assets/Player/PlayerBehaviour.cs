using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerBehaviour : MonoBehaviour
    {

        public float rotationSpeed, speed, acceleration, maxSpeed, minSpeed;

        private float verticalMovement, horizontalMovement, targetSpeed, actualSpeed;
        private Rigidbody myRigidbody;

        private void Start()
        {
            myRigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            verticalMovement = Input.GetAxis("Vertical");
            horizontalMovement = Input.GetAxis("Horizontal");

            targetSpeed = actualSpeed + (acceleration * verticalMovement * Time.deltaTime);
            References.theAccelerationBar.ShowAccelerationFraction(targetSpeed / maxSpeed);
            //transform.Rotate(transform.up, rotationSpeed * horizontalMovement * Time.fixedDeltaTime);
            //transform.Rotate(horizontalMovement * rotationSpeed * Time.fixedDeltaTime * transform.up);

            actualSpeed = Mathf.Clamp(targetSpeed, minSpeed, maxSpeed);
            myRigidbody.velocity = actualSpeed * transform.forward;

            ////WASD to move
            //Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //myRigidbody.velocity = inputVector * speed;

            //Ray rayFromCameraToCursor = References.theCamera.ScreenPointToRay(Input.mousePosition);
            //Plane playerPlane = new Plane(Vector3.up, transform.position);
            //playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera);
            //Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera);

            ////Face the new position
            //Vector3 lookAtPosition = cursorPosition;
            //transform.LookAt(lookAtPosition);


        }

        private void OnEnable()
        {
            //References.thePlayer = this;
        }
    }
}
