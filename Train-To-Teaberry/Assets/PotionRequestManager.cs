using UnityEngine;

public class PotionRequestManager : MonoBehaviour
{
    [System.Serializable]
    public class PotionRequest
    {
        public string npcName;
        public string potionName;
        public bool isCompleted = false;
    }

    public PotionRequest[] potionRequests; // List of possible requests
    public PotionRequest currentRequest; // Active request

    public void GenerateNewRequest()
    {
        // Select a random request from potionRequests
        currentRequest = potionRequests[Random.Range(0, potionRequests.Length)];
        currentRequest.isCompleted = false; // Ensure it's marked as active
    }

    public bool DeliverPotion(string potionName, InventoryController inventory)
    {
        if (currentRequest != null && !currentRequest.isCompleted)
        {
            // Debugging to check current request status
            Debug.Log($"Attempting to deliver potion: {potionName} to {currentRequest.npcName} (Requested: {currentRequest.potionName})");

            // Check for an exact match (case-insensitive)
            if (currentRequest.potionName.Equals(potionName, System.StringComparison.OrdinalIgnoreCase))
            {
                if (inventory.RemoveItem(potionName)) // Ensure the player has the potion
                {
                    Debug.Log($"Delivered {potionName} to {currentRequest.npcName}. Request completed!");
                    currentRequest.isCompleted = true;
                    GiveReward(); // Example: give gold or XP
                    return true;
                }
                else
                {
                    Debug.Log("You don't have this potion in your inventory!");
                    return false;
                }
            }
            else
            {
                Debug.Log($"Wrong potion! {currentRequest.npcName} wanted {currentRequest.potionName}, not {potionName}.");
                return false;
            }
        }
        return false;
    }

    void GiveReward()
    {
        Debug.Log("Player received gold or XP!");
        // Example: playerStats.AddMoney(50);
    }
}
