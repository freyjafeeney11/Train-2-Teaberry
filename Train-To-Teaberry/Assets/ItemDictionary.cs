using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
    public List<Item> itemPrefabs;
    private Dictionary<int, GameObject> itemDictionary;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        itemDictionary = new Dictionary<int, GameObject>();
        for(int i = 0; i < itemPrefabs.Count; i++)
        {
            if(itemPrefabs[i] !=null)
            {
                itemPrefabs[i].ID = i + 1;
            }
            foreach(Item item in itemPrefabs)
            {
                itemDictionary[item.ID] = item.gameObject;
            }
        }
    }

    // Update is called once per frame
    public GameObject GetItemPrefab(int itemID)
    {
        itemDictionary.TryGetValue(itemID, out GameObject prefab);
        if(prefab == null)
        {
            Debug.LogWarning($"Item ID {itemID} not found");

        }
        return prefab;
    }
}
