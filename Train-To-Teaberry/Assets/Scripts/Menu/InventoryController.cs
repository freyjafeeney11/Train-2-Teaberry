using UnityEngine;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel; // The panel for inventory slots
    public GameObject brewIngredientPanel; // The panel for brewing ingredient slots
    public GameObject slotPrefab; // The prefab for inventory slots
    public GameObject ingredientSlotPrefab; // The prefab for ingredient slots
    public int slotCount = 10; // Number of slots in inventory
    public GameObject[] itemPrefabs; // Items available at the start
    private List<Slot> slots = new List<Slot>(); // Inventory slots
    private List<Slot> ingredientSlots = new List<Slot>(); // Brewing ingredient slots

    void Start()
    {
        // Create the inventory slots
        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            slots.Add(slot);
        }

        // Create the brewing ingredient slots
        for (int i = 0; i < slotCount; i++)
        {
            Slot ingredientSlot = Instantiate(ingredientSlotPrefab, brewIngredientPanel.transform).GetComponent<Slot>();
            ingredientSlots.Add(ingredientSlot);
        }
    }

    // Check if an item with a matching name prefix is in either inventory
    public bool HasItem(string itemNamePrefix, bool checkIngredients = false)
    {
        List<Slot> checkList = checkIngredients ? ingredientSlots : slots;
        foreach (Slot slot in checkList)
        {
            if (slot.currentItem != null && slot.currentItem.name.StartsWith(itemNamePrefix))
            {
                Debug.Log($"Item starting with '{itemNamePrefix}' found in {(checkIngredients ? "brew ingredients" : "inventory")}!");
                return true;
            }
        }
        Debug.Log($"Item starting with '{itemNamePrefix}' not found in {(checkIngredients ? "brew ingredients" : "inventory")}!");
        return false;
    }

    // Add an item to either inventory or ingredient slots
public void AddItem(GameObject itemPrefab)
{
    bool addedToInventory = false;

    // Add to regular inventory slots
    foreach (Slot slot in slots)
    {
        if (slot.currentItem == null)
        {
            GameObject item = Instantiate(itemPrefab, slot.transform);
            item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            slot.currentItem = item;
            Debug.Log($"Added {item.name} to inventory!");
            addedToInventory = true;
            break;
        }
    }

    // If successfully added to regular inventory, add to brewing ingredient slots
    if (addedToInventory)
    {
        foreach (Slot ingredientSlot in ingredientSlots)
        {
            if (ingredientSlot.currentItem == null)
            {
                GameObject item = Instantiate(itemPrefab, ingredientSlot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                ingredientSlot.currentItem = item;
                Debug.Log($"Added {item.name} to brew ingredients!");
                break;
            }
        }
    }
    else
    {
        Debug.Log("Inventory slots are full!");
    }
}


    // Remove an item with a matching name prefix
    public bool RemoveItem(string itemNamePrefix, bool fromIngredients = false)
    {
        List<Slot> targetSlots = fromIngredients ? ingredientSlots : slots;

        foreach (Slot slot in targetSlots)
        {
            if (slot.currentItem != null && slot.currentItem.name.StartsWith(itemNamePrefix))
            {
                Debug.Log($"Removed item '{slot.currentItem.name}' from {(fromIngredients ? "brew ingredients" : "inventory")}!");
                Destroy(slot.currentItem);
                slot.currentItem = null;
                return true;
            }
        }

        Debug.Log($"No item starting with '{itemNamePrefix}' found in {(fromIngredients ? "brew ingredients" : "inventory")}!");
        return false;
    }
}
