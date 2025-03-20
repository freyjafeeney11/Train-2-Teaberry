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

    void Start()
    {
        for (int i = 0; i < shopSlotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, shopPanel.transform).GetComponent<Slot>();
            shopSlots.Add(slot);
        }

        // Assign different prices to different mushrooms
        for (int i = 0; i < itemPrefabs.Length && i < shopSlots.Count; i++)
        {
            int price = 10; // Default price

            if (itemPrefabs[i].name.Contains("Green Mushroom")) price = 20;
            if (itemPrefabs[i].name.Contains("White Mushroom")) price = 30;

            AddToShop(itemPrefabs[i], i, initialStackSize, price);
        }
    }


public void AddToShop(GameObject itemPrefab, int index, int quantity, int price)
{
    if (index < shopSlots.Count)
    {
        GameObject item = Instantiate(itemPrefab, shopSlots[index].transform);
        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        ShopItem shopItem = item.AddComponent<ShopItem>(); // Add ShopItem script
        shopItem.shopController = this;
        shopItem.itemPrefab = itemPrefab;
        shopItem.quantity = quantity; // Set the initial quantity
        shopItem.price = price; // Set the item's price

        Debug.Log($"{itemPrefab.name} added to shop with price: {price}");
    }
}


    // Reduce item quantity and remove it from the shop when sold out
public void BuyItem(GameObject itemPrefab, ShopItem shopItem)
{
    if (playerInventory != null && shopItem.quantity > 0)
    {
        int itemPrice = shopItem.price; // Get the specific price for this item

        if (playerStats != null && playerStats.SpendMoney(itemPrice))
        {
            playerInventory.AddItem(itemPrefab);
            shopItem.quantity--;

            Debug.Log($"Bought 1 {itemPrefab.name} for {itemPrice} gold. Remaining: {shopItem.quantity}");

            if (shopItem.quantity <= 0)
            {
                Destroy(shopItem.gameObject);
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
