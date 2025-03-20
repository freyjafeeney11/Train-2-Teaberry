using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Player's money variable
    public int playerMoney = 1000;

    // Method to add money
    public void AddMoney(int amount)
    {
        playerMoney += amount;
        Debug.Log("Money added: " + amount + ". Total: " + playerMoney);
    }

    // Method to spend money
    public bool SpendMoney(int amount)
    {
        if (playerMoney >= amount)
        {
            playerMoney -= amount;
            Debug.Log("Money spent: " + amount + ". Total: " + playerMoney);
            return true; // Successfully spent money
        }
        else
        {
            Debug.Log("Not enough money to spend!");
            return false; // Not enough money
        }
    }
}
