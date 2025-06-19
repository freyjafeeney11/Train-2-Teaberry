using System.Collections;
using UnityEngine;

public class LeaveRoom : MonoBehaviour
{
    public Transform exitSpawn; // New room spawn point
    public MoveRoomTransition screenFader; // Assign in Inspector

    private bool playerIsClose = false;
    private GameObject player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && player != null)
        {
            // Start the fade + teleport coroutine
            StartCoroutine(FadeAndTeleport());
        }
    }

private IEnumerator FadeAndTeleport()
{
    // Fade to black
    screenFader.FadeToBlack(null);

    // Wait for the fade to complete
    yield return new WaitForSeconds(screenFader.fadeDuration);

    // (Teleport player or move camera here)
    player.transform.position = exitSpawn.position;

    // WAIT extra time here to keep screen black longer
    yield return new WaitForSeconds(1f); // <-- add this line, adjust seconds as you like

    // Fade back in
    screenFader.FadeFromBlack(null);
}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            player = null;
        }
    }
}
