using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Transform roomSpawnPoint; // Drag the spawn point for the new room in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure your character has the tag "Player"
        {
            // Move the character to the new room's spawn point
            other.transform.position = roomSpawnPoint.position;

            // Optional: Add additional logic for entering the room, such as animations
            Debug.Log("Character entered the room!");
        }
    }
}
