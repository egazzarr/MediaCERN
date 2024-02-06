using UnityEngine;

public class ShiveringEffect : MonoBehaviour
{
    public float shiverSpeed = 1f;      // Adjust the speed of the shivering effect
    public float shiverMagnitude = 0.1f; // Adjust the magnitude of the shivering effect
    public float randomOffset = 0.05f;   // Adjust the amount of randomness

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Apply a shivering effect using Mathf.Sin and add randomness
        float shiverOffsetX = Mathf.Sin(Time.time * shiverSpeed) * shiverMagnitude;
        float shiverOffsetY = Mathf.Sin(Time.time * shiverSpeed) * shiverMagnitude;
        float randomOffsetX = Random.Range(-randomOffset, randomOffset);
        float randomOffsetY = Random.Range(-randomOffset, randomOffset);

        // Combine the shivering effect and randomness
        float finalOffsetX = shiverOffsetX + randomOffsetX;
        float finalOffsetY = shiverOffsetY + randomOffsetY;

        transform.position = initialPosition + new Vector3(finalOffsetX, finalOffsetY, 0f);
    }
}