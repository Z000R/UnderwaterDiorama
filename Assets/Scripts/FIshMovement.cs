using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIshMovement : MonoBehaviour
{
    public Vector3 boxCenter = Vector3.zero; // Center of the stationary box
    public Vector3 boxSize = new Vector3(10, 10, 10);
    public float speed = 1.0f;
    public float noiseScale = 0.5f;

    private float offsetX;
    private float offsetY;
    private float offsetZ;

    void Start()
    {
        offsetX = Random.Range(0f, 100f);
        offsetY = Random.Range(0f, 100f);
        offsetZ = Random.Range(0f, 100f);
    }

    void Update()
    {
        float time = Time.time * speed;

        float x = Mathf.PerlinNoise(time * noiseScale + offsetX, 0) * boxSize.x - boxSize.x / 2;
        float y = Mathf.PerlinNoise(time * noiseScale + offsetY, 1) * boxSize.y - boxSize.y / 2;
        float z = Mathf.PerlinNoise(time * noiseScale + offsetZ, 2) * boxSize.z - boxSize.z / 2;

        Vector3 newPos = new Vector3(x, y, z) + boxCenter;
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}