using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;   // Reference to the player's transform
    public Vector3 cameraOffset = new Vector3(0f, 1.5f, -3f);   // Offset for camera position relative to player
    public float followSpeed = 5f;      // Speed of camera movement (higher values mean faster movement)

    private Vector3 targetPosition;     // Desired position for the camera to move towards
    private Quaternion targetRotation;  // Desired rotation for the camera to rotate towards

    void Start()
    {
        // Initialize target position and rotation based on initial setup
        if (playerTransform != null)
        {
            targetPosition = playerTransform.position + playerTransform.TransformDirection(cameraOffset);
            targetRotation = Quaternion.LookRotation(playerTransform.position - targetPosition, playerTransform.up);
        }
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Calculate target position based on player's position and offset
            Vector3 desiredPosition = playerTransform.position + playerTransform.TransformDirection(cameraOffset);

            // Smoothly interpolate towards the desired position
            targetPosition = Vector3.Lerp(targetPosition, desiredPosition, Time.fixedDeltaTime * followSpeed);

            // Set the camera's position to the interpolated target position
            transform.position = targetPosition;

            // Calculate target rotation to look at the player's position
            Quaternion desiredRotation = Quaternion.LookRotation(playerTransform.position - targetPosition, playerTransform.up);

            // Smoothly interpolate towards the desired rotation
            targetRotation = Quaternion.Slerp(targetRotation, desiredRotation, Time.fixedDeltaTime * followSpeed);

            // Set the camera's rotation to the interpolated target rotation
            transform.rotation = targetRotation;
        }
    }
}