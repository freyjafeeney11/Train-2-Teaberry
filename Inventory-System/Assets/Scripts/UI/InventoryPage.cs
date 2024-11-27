using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    private InventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;
    [SerializeField]
    private InventoryDescription itemDescription;

    List<InventoryItem> listofUIItems = new List<InventoryItem>();

    public Sprite image;
    public int quantity;
    public string title, description;

    private void Awake()
    {
        Hide();
        itemDescription.ResetDescription();
    }
    public void InitializeInventoryUI(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            InventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listofUIItems.Add(uiItem);

            // Actions
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();
        listofUIItems[0].SetData(image, quantity);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void HandleItemSelection(InventoryItem obj)
    {
        itemDescription.SetDescription(image, title, description);
        listofUIItems[0].Select();
    }
    public void HandleBeginDrag(InventoryItem obj)
    {
        
    }
    public void HandleSwap(InventoryItem obj)
    {
        
    }
    public void HandleEndDrag(InventoryItem obj)
    {
        
    }
    public void HandleShowItemActions(InventoryItem obj)
    {
        
    }
}
