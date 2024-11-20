using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    private InventoryItem itemPrefabs;

    [SerializeField]
    private RectTransform contentPanel;

    List<InventoryItem> listofItems = new List<InventoryItem>();

    public void InitializeInventoryUI(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            InventoryItem uiItem = Instantiate(itemPrefabs, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listofItems.Add(uiItem);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
