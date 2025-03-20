using UnityEngine;
using System.Collections.Generic;

public class ShopController : MonoBehaviour
{
    public GameObject shopPanel; // The panel where shop items will be displayed
    public GameObject slotPrefab; // The prefab used for each shop slot
    public int shopSlotCount = 10; // Fixed number of slots in the shop
    public GameObject[] itemPrefabs; // Predefined items for sale
    private List<Slot> shopSlots = new List<Slot>(); // List to store shop slots

    public InventoryController playerInventory; // Reference to player's inventory

    void Start()
    {
        // Create shop slots dynamically
        for (int i = 0; i < shopSlotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, shopPanel.transform).GetComponent<Slot>();
            shopSlots.Add(slot);
        }

        // Populate shop with items
        for (int i = 0; i < itemPrefabs.Length && i < shopSlots.Count; i++)
        {
            AddToShop(itemPrefabs[i], i);
        }
    }

    // Add an item to the shop slot
    public void AddToShop(GameObject itemPrefab, int index)
    {
        if (index < shopSlots.Count)
        {
            GameObject item = Instantiate(itemPrefab, shopSlots[index].transform);
            item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            ShopItem shopItem = item.AddComponent<ShopItem>(); // Dynamically attach ShopItem
            shopItem.shopController = this;
            shopItem.itemPrefab = itemPrefab; // Link the item prefab
        }
    }

    // Add an item to the player's inventory
    public void AddToInventory(GameObject itemPrefab)
    {
        if (playerInventory != null)
        {
            playerInventory.AddItem(itemPrefab);
            Debug.Log($"Added {itemPrefab.name} to inventory!");
        }
        else
        {
            Debug.LogError("Player inventory not assigned to ShopController!");
        }
    }
}
