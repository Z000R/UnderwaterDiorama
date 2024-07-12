using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaController_Script : MonoBehaviour
{
    public float accelerationY;
    public float speed = 10f;
    public float maxSpeed = 50f;
    public float horizontalTorqueForce = 100f;
    public float verticalTorqueForce = 100f;
    public float maxTorque = 50f;

    private Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 externalAcceleration = new Vector3(0, accelerationY, 0);
        rb.AddForce(externalAcceleration, ForceMode.Acceleration);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate torque forces 
        float yTorque = - horizontalInput * horizontalTorqueForce; // Torque around y-axis (horizontal)
        float xTorque = verticalInput * verticalTorqueForce;   // Torque around x-axis (vertical)

        Vector3 torque = new Vector3(xTorque, yTorque, 0f);

        // Cap torque force
        if (torque.magnitude > maxTorque)
        {
            torque = torque.normalized * maxTorque;
        }

        // Apply torque 
        rb.AddTorque(transform.TransformDirection(torque));

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            // Calculate acceleration force
            Vector3 acceleration = transform.up * speed;

            // Cap acceleration force
            if (acceleration.magnitude > maxSpeed)
            {
                acceleration = acceleration.normalized * maxSpeed;
            }

            // Apply acceleration 
            rb.AddForce(acceleration, ForceMode.Acceleration);
        }
    }
}