using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string targetScene; // Target scene to load
    public GameObject portal;  // Assign the portal GameObject in the Inspector
    public float interactionDistance = 2.0f; // Distance to interact with the portal

    private GameObject player; // Reference to the player GameObject
    private bool isPlayerInRange = false; // Tracks if the player is near the portal

    void Start()
    {
        // Warn if the target scene is not set
        if (string.IsNullOrEmpty(targetScene))
        {
            Debug.LogWarning("Target scene is not set. Please assign it in the Inspector.");
        }

        // Find the player GameObject by tag
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject not found! Ensure the player is tagged as 'Player'.");
        }
    }

    void Update()
    {
        // Continuously check for interaction when the player is in range
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.H))
        {
            LoadTargetScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the portal's trigger range
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered portal range. Press 'H' to interact.");
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player exits the portal's trigger range
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player left portal range.");
            isPlayerInRange = false;
        }
    }

    private void LoadTargetScene()
    {
        // Ensure the target scene is set
        if (string.IsNullOrEmpty(targetScene))
        {
            Debug.LogError("Target scene is not specified. Please assign it in the Inspector.");
            return;
        }

        // Log scene change and load the scene
        Debug.Log($"Loading Scene: {targetScene}");
        SceneManager.LoadScene(targetScene);
    }
}
