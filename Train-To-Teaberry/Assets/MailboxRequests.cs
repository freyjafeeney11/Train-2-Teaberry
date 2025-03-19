using UnityEngine;
using UnityEngine.UI;

public class MailboxRequests : MonoBehaviour
{
    public PotionRequestManager potionRequestManager; // Reference to the PotionRequestManager
    public KeyCode interactKey = KeyCode.R;            // Key to interact with the mailbox (e.g., 'E' key)

    public Text potionRequestText; // Reference to the Text component in the Guide Tab

    private GameObject player; // Reference to the player object

    private void Start()
    {
        // Get reference to the player object
        player = GameObject.FindGameObjectWithTag("Player"); // Assuming the player has the tag "Player"
        if (player == null)
        {
            Debug.LogError("Player object not found! Ensure the player is tagged with 'Player'.");
        }

        // Ensure the potion request text starts empty
        if (potionRequestText != null)
        {
            potionRequestText.text = "No requests yet.";
        }
    }

    private void Update()
    {
        // Check if the player is close enough and presses the interact key
        if (IsPlayerInRange() && Input.GetKeyDown(interactKey))
        {
            Debug.Log("Player is close");
            ReceivePotionRequest();
        }
    }

    // Simple check to see if the player is within a certain range of the mailbox
    private bool IsPlayerInRange()
    {
        if (player != null)
        {
            // Check if the distance between the mailbox and the player is within range (3 units)
            return Vector3.Distance(transform.position, player.transform.position) < 3f;
        }
        return false;
    }

    // Method to trigger receiving a potion request
    private void ReceivePotionRequest()
    {
        // Get a random potion request from the PotionRequestManager
        string newPotionRequest = potionRequestManager.GetRandomPotionRequest();

        if (!string.IsNullOrEmpty(newPotionRequest))
        {
            Debug.Log("New potion request received: " + newPotionRequest);

            // Update the Guide Tab UI with the new potion request
            if (potionRequestText != null)
            {
                potionRequestText.text = "Potion Request: " + newPotionRequest;
            }
        }
    }
}
