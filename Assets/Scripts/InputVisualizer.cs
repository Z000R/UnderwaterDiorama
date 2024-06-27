using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputVisualizer : MonoBehaviour
{
    // GameObjects representing the directions and spacebar
    public GameObject upObject;
    public GameObject downObject;
    public GameObject leftObject;
    public GameObject rightObject;
    public GameObject spacebarObject; // New GameObject for Spacebar

    // Materials for the keys
    public Material defaultMaterial;
    public Material glowMaterial;
    public Material spacebarGlowMaterial; // New material for Spacebar

    void Update()
    {
        UpdateKeyVisuals();
    }

    public void UpdateKeyVisuals()
    {
        // Get the input values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Handle the visualization for each direction based on input
        HandleKeyVisualization(verticalInput > 0, upObject, glowMaterial);
        HandleKeyVisualization(verticalInput < 0, downObject, glowMaterial);
        HandleKeyVisualization(horizontalInput < 0, leftObject, glowMaterial);
        HandleKeyVisualization(horizontalInput > 0, rightObject, glowMaterial);
        HandleKeyVisualization(Input.GetKey(KeyCode.Space), spacebarObject, spacebarGlowMaterial); // Handle Spacebar
    }

    private void HandleKeyVisualization(bool isActive, GameObject keyObject, Material activeMaterial)
    {
        if (keyObject == null)
            return;

        var renderer = keyObject.GetComponent<Renderer>();
        if (renderer == null)
            return;

        if (isActive)
        {
            renderer.material = activeMaterial;
        }
        else
        {
            renderer.material = defaultMaterial;
        }
    }
}