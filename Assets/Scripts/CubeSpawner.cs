using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [Header("Cube Settings")]
    [SerializeField] private int numberOfCubes = 10; // Number of cubes to spawn
    [SerializeField] private Vector2 sizeRange = new Vector2(1f, 3f); // Range for random cube sizes

    [Header("Spawn Bounds")]
    [SerializeField] private Vector3 spawnAreaMin = new Vector3(-10f, 0f, -10f); // Minimum bounds for spawning
    [SerializeField] private Vector3 spawnAreaMax = new Vector3(10f, 5f, 10f); // Maximum bounds for spawning

    [Header("Cube Prefab")]
    [SerializeField] private GameObject cubePrefab; // Prefab for the cubes

    private void Start()
    {
        if (cubePrefab == null)
        {
            Debug.LogError("Cube prefab not assigned.");
            return;
        }

        SpawnCubes();
    }

    private void SpawnCubes()
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            // Generate random position within the specified bounds
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );

            // Instantiate the cube
            GameObject cube = Instantiate(cubePrefab, randomPosition, Quaternion.identity);

            // Generate random size within the specified range
            float randomSize = Random.Range(sizeRange.x, sizeRange.y);
            cube.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

            // Generate random rotation
            Vector3 randomRotation = new Vector3(
                Random.Range(0f, 360f),
                Random.Range(0f, 360f),
                Random.Range(0f, 360f)
            );
            cube.transform.eulerAngles = randomRotation;
        }
    }

    private void OnDrawGizmos()
    {
        // Set the color of the Gizmos
        Gizmos.color = Color.green;

        // Draw a wire cube representing the spawn area
        Vector3 center = (spawnAreaMin + spawnAreaMax) / 2;
        Vector3 size = spawnAreaMax - spawnAreaMin;
        Gizmos.DrawWireCube(center, size);
    }

    private void OnDrawGizmosSelected()
    {
        // Set the color of the Gizmos
        Gizmos.color = Color.red;

        // Draw a solid cube representing the spawn area when selected
        Vector3 center = (spawnAreaMin + spawnAreaMax) / 2;
        Vector3 size = spawnAreaMax - spawnAreaMin;
        Gizmos.DrawCube(center, size);
    }
}