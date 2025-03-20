using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerClickHandler
{
    public GameObject itemPrefab; 
    public ShopController shopController;
    public int quantity;
    public int price; // Price of the item

    private Text quantityText;
    private Text priceText;

    void Start()
    {
        quantityText = transform.Find("QuantityText")?.GetComponent<Text>();
        priceText = transform.Find("PriceText")?.GetComponent<Text>();

        UpdateUI();
    }

    // Method to update the item's UI display
    public void UpdateUI()
    {
        if (quantityText != null)
        {
            quantityText.text = "x" + quantity.ToString();
        }

        if (priceText != null)
        {
            priceText.text = price + " gold";
        }
    }

    // Called when the item is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if (shopController != null)
        {
            shopController.BuyItem(itemPrefab, this);
        }
    }
}
