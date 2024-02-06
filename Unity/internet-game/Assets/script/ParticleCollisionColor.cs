using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionColor : MonoBehaviour
{
    private ParticleSystem particleSystem;

    void Start()
    {
        // Get the ParticleSystem component attached to the GameObject
        particleSystem = GetComponent<ParticleSystem>();

        // Check if the ParticleSystem component exists
        if (particleSystem == null)
        {
            Debug.LogError("Particle System not found on GameObject.");
        }

        // Enable collision events
        var collisionModule = particleSystem.collision;
        collisionModule.enabled = true;
    }

    void OnParticleCollision(GameObject other)
    {
        // Check if the object collided with has a Renderer component
        Renderer renderer = other.GetComponent<Renderer>();

        if (renderer != null)
        {
            // Change the color of the particles when they collide with the object
            var mainModule = particleSystem.main;
            mainModule.startColor = new Color(Random.value, Random.value, Random.value);
        }
    }
}
