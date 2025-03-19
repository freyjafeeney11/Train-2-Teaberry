using System.Collections.Generic;
using UnityEngine;

public class MailboxRequests : MonoBehaviour
{
    public List<PotionRequest> activeRequests = new List<PotionRequest>();
    public List<string> potionNames = new List<string> { "Healing Potion", "Mana Potion", "Strength Elixir" };
    public List<string> npcNames = new List<string> { "Elder Rowan", "Merchant Lin", "Knight Theo" };

    public void GenerateRequests()
    {
        activeRequests.Clear();
        for (int i = 0; i < 3; i++) // Generate 3 random requests
        {
            PotionRequest newRequest = new PotionRequest
            {
                potionName = potionNames[Random.Range(0, potionNames.Count)],
                recipientName = npcNames[Random.Range(0, npcNames.Count)]
            };
            activeRequests.Add(newRequest);
        }
        Debug.Log("New potion requests available!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GenerateRequests();
            // Optionally update UI here
        }
    }
}
