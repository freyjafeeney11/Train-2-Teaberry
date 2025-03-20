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
    public void SatisfyRequest() {
        currentRequest.isCompleted = true;
    }

    void GiveReward()
    {
        Debug.Log("Player received gold or XP!");
        // Example: playerStats.AddMoney(50);
    }
}
