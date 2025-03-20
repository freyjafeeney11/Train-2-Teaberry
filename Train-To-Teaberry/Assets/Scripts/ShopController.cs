using UnityEngine;
using System.Collections.Generic;

public class ShopController : MonoBehaviour
{
    public GameObject shopPanel; // Panel for shop items
    public GameObject slotPrefab; // Prefab for shop slots
    public int shopSlotCount = 10; // Number of shop slots
    public GameObject[] itemPrefabs; // Item prefabs available for sale
    private List<Slot> shopSlots = new List<Slot>(); // List of shop slots

    public InventoryController playerInventory; // Reference to player's inventory
    public PlayerStats playerStats; // Reference to PlayerStats for money management
    public int initialStackSize = 3; // Default stack size for each shop slot
    public int mushroomCost = 10; // Cost of a mushroom

    void Start()
    {
        // Create shop slots dynamically
        for (int i = 0; i < shopSlotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, shopPanel.transform).GetComponent<Slot>();
            shopSlots.Add(slot);
        }

        // Populate shop slots with items and quantities
        for (int i = 0; i < itemPrefabs.Length && i < shopSlots.Count; i++)
        {
            AddToShop(itemPrefabs[i], i, initialStackSize); // Add 3 items to each slot
        }
    }

    // Add an item with a stack size to the shop slot
    public void AddToShop(GameObject itemPrefab, int index, int quantity)
    {
        if (index < shopSlots.Count)
        {
            GameObject item = Instantiate(itemPrefab, shopSlots[index].transform);
            item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            ShopItem shopItem = item.AddComponent<ShopItem>(); // Dynamically add ShopItem script
            shopItem.shopController = this;
            shopItem.itemPrefab = itemPrefab;
            shopItem.quantity = quantity; // Set the initial quantity
        }
    }

    // Reduce item quantity and remove it from the shop when sold out
    public void BuyItem(GameObject itemPrefab, ShopItem shopItem)
    {
        if (playerInventory != null && shopItem.quantity > 0)
        {
            if (playerStats != null && playerStats.SpendMoney(mushroomCost))
            {
                playerInventory.AddItem(itemPrefab); // Add item to inventory
                shopItem.quantity--; // Decrease the stack size

                Debug.Log($"Bought 1 {itemPrefab.name} for {mushroomCost} gold. Remaining: {shopItem.quantity}");

                // If the stack is empty, remove the item from the shop
                if (shopItem.quantity <= 0)
                {
                    Destroy(shopItem.gameObject); // Remove from the shop UI
                    Debug.Log($"{itemPrefab.name} is sold out!");
                }
            }
            else
            {
                Debug.LogWarning("Not enough money to buy this item!");
            }
        }
        else
        {
            Debug.LogWarning("Item cannot be purchased. Either the inventory is missing or the stack is empty.");
        }
    }
}
