using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskInteraction : MonoBehaviour
{
    public Camera deskCamera; // Camera for desk POV (e.g., showing your drawing)
    public Camera mainCamera; // Main gameplay camera
    public GameObject deskObject; // Assign the desk GameObject in the Inspector
    public float interactionDistance = 2.0f; // Distance to interact with the desk
    private GameObject player; // Reference to the player GameObject
    private TopDownMovement playerMovement; // Reference to the player movement script

    void Start()
    {
        // Ensure the desk camera starts disabled
        deskCamera.enabled = false;
        mainCamera.enabled = true;

        // Find the player and get the PlayerMovement script (if not already set)
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<TopDownMovement>();
        }
        else
        {
            Debug.LogWarning("No GameObject tagged as 'Player' found!");
        }
    }

    void Update()
    {
        // Check for interaction if the player is near the desk
        if (Input.GetKeyDown(KeyCode.H) && IsPlayerNearDesk())
        {
            if (!deskCamera.enabled)
            {
                EnterDeskView();
            }
            else
            {
                ExitDeskView();
            }
        }
    }

    private bool IsPlayerNearDesk()
    {
        // Ensure the player is close to the desk
        float distance = Vector3.Distance(player.transform.position, deskObject.transform.position);
        return distance <= interactionDistance;
    }

    private void EnterDeskView()
    {
        // Enable the desk camera and disable the main camera
        deskCamera.enabled = true;
        mainCamera.enabled = false;

        // Disable player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = false; // Disables player movement controls
        }

        Debug.Log("Entered desk view.");
    }

    private void ExitDeskView()
    {
        // Enable the main camera and disable the desk camera
        deskCamera.enabled = false;
        mainCamera.enabled = true;

        // Re-enable player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = true; // Re-enables player movement controls
        }

        Debug.Log("Exited desk view.");
    }

    // Optional: Use this to visualize the interaction range in the scene
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(deskObject.transform.position, interactionDistance);
    }
}
