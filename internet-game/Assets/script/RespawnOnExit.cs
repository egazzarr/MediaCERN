using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOnExit : MonoBehaviour
{
    public GameObject prefabToRespawn; // Reference to the prefab of the sprite you want to respawn.

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Boundary")) // Assuming you have a boundary trigger.
        {
            // Get the position of the current sprite.
            Vector3 currentPosition = transform.position;

            // Instantiate a new sprite at the same position.
            Instantiate(prefabToRespawn, currentPosition, Quaternion.identity);
        }
    }
}