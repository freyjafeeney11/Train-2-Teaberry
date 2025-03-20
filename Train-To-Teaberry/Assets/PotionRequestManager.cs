using UnityEngine;

public class PotionRequestManager : MonoBehaviour
{
    public PlayerStats moneySystem; // Reference to PlayerStats for money management

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
    public void SatisfyRequest() {
        currentRequest.isCompleted = true;
        GiveReward();
    }

    void GiveReward()
    {
        Debug.Log("Player received gold!");
        moneySystem.AddMoney(50);
        // Example: playerStats.AddMoney(50);
    }
}
