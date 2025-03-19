using UnityEngine;

public class PotionRequestManager : MonoBehaviour
{
    public string[] potionRequests = { "Health Potion", "Fart Potion", "Mana Potion" }; // Example requests

    public string GetRandomPotionRequest()
    {
        if (potionRequests.Length > 0)
        {
            // Randomly select a potion request
            return potionRequests[Random.Range(0, potionRequests.Length)];
        }
        return null;  // No request if the array is empty
    }
}
