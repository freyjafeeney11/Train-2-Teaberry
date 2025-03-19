using UnityEngine;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel; // The panel where inventory slots will be displayed
    public GameObject slotPrefab; // The prefab used for each inventory slot
    public int slotCount = 10; // Set a fixed number of slots for the inventory
    public GameObject[] itemPrefabs; // Predefined items (e.g., potions) to be placed in inventory
    private List<Slot> slots = new List<Slot>(); // List to store references to each slot

    void Start()
    {
        // Create the inventory slots based on the fixed `slotCount`
        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            slots.Add(slot); // Store the slot reference

            if (i < itemPrefabs.Length)
            {
                // Add starting items (only as long as itemPrefabs has entries)
                //AddItem(Instantiate(itemPrefabs[i]));
            }
        }
    }

    // Check if an item is already in the inventory by name
    public bool HasItem(string itemName)
    {
        foreach (Slot slot in slots)
        {
            if (slot.currentItem != null && slot.currentItem.name == itemName)
            {
                return true;
            }
        }
        return false;
    }

    // Add an item to the first available slot in the inventory
    public void AddItem(GameObject itemPrefab)
    {
        // Find an empty slot and place the item there
        foreach (Slot slot in slots)
        {
            if (slot.currentItem == null) // Look for an empty slot
            {
                GameObject item = Instantiate(itemPrefab, slot.transform); // Instantiate the item
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Position the item in the slot
                slot.currentItem = item; // Set the current item for this slot
                Debug.Log($"Added {item.name} to inventory!");
                return; // Exit after adding the item
            }
        }

        // If the inventory is full, log a message
        Debug.Log("Inventory is full!");
    }

    // Remove an item by name from the inventory
    public void RemoveItem(string itemName)
    {
        foreach (Slot slot in slots)
        {
            if (slot.currentItem != null && slot.currentItem.name == itemName)
            {
                Destroy(slot.currentItem); // Destroy the item GameObject
                slot.currentItem = null; // Set the slot's current item to null
                Debug.Log($"Removed {itemName} from inventory!");
                return; // Exit after removing the item
            }
        }

        // If the item is not found, log a message
        Debug.Log($"{itemName} not found in inventory!");
    }
}
