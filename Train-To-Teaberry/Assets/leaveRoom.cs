using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveRoom : MonoBehaviour
{
    public Transform exitSpawn; // Drag the spawn point for the new room in the Inspector
    private bool playerIsClose = false; // Tracks if the player is in range
    private GameObject player; // Reference to the player object

    void Update()
    {
        // Check if the player is close and the 'E' key is pressed
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && player != null)
        {
            player.transform.position = exitSpawn.position; // Move the player to the new spawn point
            Debug.Log("Left room");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            playerIsClose = true; // Player is within the trigger
            player = other.gameObject; // Cache the player reference
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the exiting object is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            playerIsClose = false; // Player left the trigger area
            player = null; // Clear the player reference
        }
    }
}
