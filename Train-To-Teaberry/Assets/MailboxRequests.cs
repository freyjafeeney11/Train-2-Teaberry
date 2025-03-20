using UnityEngine;
using UnityEngine.UI;

public class MailboxRequests : MonoBehaviour
{
    public PotionRequestManager potionRequestManager; // Reference to the PotionRequestManager
    public KeyCode interactKey = KeyCode.R;            // Key to interact with the mailbox (e.g., 'R' key)

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
        else
        {
            Debug.LogError("PotionRequestText component not assigned!");
        }
    }

    private void Update()
    {
        // Check if the player is close enough and presses the interact key
        if (IsPlayerInRange() && Input.GetKeyDown(interactKey))
        {
            Debug.Log("Player is close, receiving potion request.");
            ReceivePotionRequest();
        }

        // Check if the current potion request is completed
        if (potionRequestManager != null && potionRequestManager.currentRequest != null && potionRequestManager.currentRequest.isCompleted)
        {
            Debug.Log("Potion request completed, updating UI.");
            potionRequestText.text = "No active requests.";
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
        if (potionRequestManager == null)
        {
            Debug.LogError("PotionRequestManager reference is missing!");
            return;
        }

        // Ensure we have an active potion request
        potionRequestManager.GenerateNewRequest(); // Make sure this is actually called and executed
        Debug.Log("called gen req in mailbox req");
        if (potionRequestManager.currentRequest != null)
        {
            Debug.Log("New potion request received: " + potionRequestManager.currentRequest.potionName);

            // Update the Guide Tab UI with the new potion request
            if (potionRequestText != null)
            {
                potionRequestText.text = potionRequestManager.currentRequest.npcName + " wants a " + potionRequestManager.currentRequest.potionName;
            }
            else
            {
                Debug.LogError("PotionRequestText component is missing!");
            }
        }
        else
        {
            Debug.Log("No available potion requests.");
        }
    }
}
