using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerClickHandler
{
    public GameObject itemPrefab; // Reference to the prefab for this shop item
    public ShopController shopController; // Reference to the ShopController
    public int quantity; // Current quantity of the item stack

    private Text quantityText; // UI element to show the item quantity

    void Start()
    {
        // Find or add a Text component to display the quantity
        quantityText = GetComponentInChildren<Text>();
        UpdateQuantityDisplay();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (shopController != null && itemPrefab != null && quantity > 0)
        {
            shopController.BuyItem(itemPrefab, this); // Trigger purchase
            UpdateQuantityDisplay(); // Update the UI after purchase
        }
        else
        {
            Debug.LogWarning("Cannot buy this item. It may be sold out!");
        }
    }

    // Update the quantity display on the shop slot
    private void UpdateQuantityDisplay()
    {
        if (quantityText != null)
        {
            quantityText.text = quantity > 0 ? $"x{quantity}" : "Sold Out";
        }
    }
}
