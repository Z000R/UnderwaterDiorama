using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaController_Script : MonoBehaviour
{
    public float accelerationY;
    public float speed = 10f;
    public float maxSpeed = 50f;
    public float torqueForce = 100f;
    public float maxTorque = 50f;
    public Vector3 targetVector = Vector3.forward; // Desired orientation vector
    public float orientationSpeed = 1f; // Speed of orientation correction

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

        // Calculate torque force 
        Vector3 torque = new Vector3(verticalInput * torqueForce, 0f, -horizontalInput * torqueForce);

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

        // Apply orientation correction towards the target vector
        ApplyOrientationCorrection();
    }

    void ApplyOrientationCorrection()
    {
     
        Quaternion targetRotation = Quaternion.LookRotation(targetVector);
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, orientationSpeed * Time.deltaTime);
    }
}