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

    [SerializeField]
    private MouseFollower mouseFollower;

    List<InventoryItem> listofUIItems = new List<InventoryItem>();

    public Sprite image, image2;
    public int quantity;
    public string title, description;

    private int currentlyDraggedItemIndex = -1;

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
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
        listofUIItems[1].SetData(image2, quantity);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void HandleItemSelection(InventoryItem inventoryItemUI)
    {
        itemDescription.SetDescription(image, title, description);
        listofUIItems[0].Select();
    }
    public void HandleBeginDrag(InventoryItem inventoryItemUI)
    {
        int index = listofUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
            return;
        currentlyDraggedItemIndex = index;

        mouseFollower.Toggle(true);
        mouseFollower.SetData(index == 0 ? image : image2, quantity);
    }
    public void HandleSwap(InventoryItem inventoryItemUI)
    {
        int index = listofUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
            return;
        }
        listofUIItems[currentlyDraggedItemIndex].SetData(index == 0 ? image : image2, quantity);
        listofUIItems[index].SetData(currentlyDraggedItemIndex == 0 ? image : image2, quantity);
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;

    }
    public void HandleEndDrag(InventoryItem inventoryItemUI)
    {
        mouseFollower.Toggle(false);
    }
    public void HandleShowItemActions(InventoryItem inventoryItemUI)
    {
        
    }
}
