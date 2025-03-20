using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerClickHandler
{
    public GameObject itemPrefab; // Reference to the prefab for this shop item
    public ShopController shopController; // Reference to the ShopController

    // This is triggered when the player clicks on the item
    public void OnPointerClick(PointerEventData eventData)
    {
        if (shopController != null && itemPrefab != null)
        {
            shopController.AddToInventory(itemPrefab); // Add item to player's inventory
            Debug.Log($"Clicked on {itemPrefab.name}, adding to inventory.");
        }
        else
        {
            Debug.LogError("ShopController or itemPrefab is not assigned!");
        }
    }
}
