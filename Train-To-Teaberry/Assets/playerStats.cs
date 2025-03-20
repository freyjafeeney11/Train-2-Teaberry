using UnityEngine;
using UnityEngine.UI; // For legacy UI
// using TMPro; // Uncomment if using TextMeshPro

public class PlayerStats : MonoBehaviour
{
    // Player's money variable
    public int playerMoney = 1000;

    // UI Text for displaying money
    public Text moneyAmount; // Use TextMeshProUGUI for TextMeshPro
    
        void Start()
    {
        UpdateMoneyText(); // Initialize the UI with the starting amount
    }
    // Method to add money
    public void AddMoney(int amount)
    {
        playerMoney += amount;
        Debug.Log("Money added: " + amount + ". Total: " + playerMoney);
        UpdateMoneyText(); // Update the UI
    }


    // Method to spend money
    public bool SpendMoney(int amount)
    {
        if (playerMoney >= amount)
        {
            playerMoney -= amount;
            Debug.Log("Money spent: " + amount + ". Total: " + playerMoney);
            UpdateMoneyText(); // Update the UI
            return true;
        }
        else
        {
            Debug.Log("Not enough money to spend!");
            return false;
        }
    }

void UpdateMoneyText()
{
    if (moneyAmount != null)
    {
        moneyAmount.text = "$" + playerMoney.ToString();
        Debug.Log("Money text updated: " + moneyAmount.text); // Add this line
    }
    else
    {
        Debug.Log("moneyAmount is null! Check the UI assignment.");
    }
}

}
