using UnityEngine;
using System;
using System.Collections.Generic;
using Inventory.UI;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private List<ModelInventoryItem> inventoryItems;

        [field: SerializeField]
        public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, ModelInventoryItem>> OnInventoryUpdated;

        public void Initialize()
        {
            inventoryItems = new List<ModelInventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(ModelInventoryItem.GetEmptyItem());
            }
        }

        public void AddItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if(inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = new ModelInventoryItem
                    {
                        item = item,
                        quantity = quantity
                    };
                    return;
                }
            }
        }

        public void AddItem(ModelInventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public Dictionary<int, ModelInventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, ModelInventoryItem> returnValue = new Dictionary<int, ModelInventoryItem>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }
        public ModelInventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            ModelInventoryItem item1 = inventoryItems[itemIndex_1];
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item1;
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
    }

    [Serializable]
    public struct ModelInventoryItem
    {
        public int quantity;
        public ItemSO item;
        public bool IsEmpty => item == null;
        
        public ModelInventoryItem ChangeQuantity(int newQuantity)
        {
            return new ModelInventoryItem
            {
                item = this.item,
                quantity = newQuantity,
            };
        }
        public static ModelInventoryItem GetEmptyItem() => new ModelInventoryItem
        {
            item = null,
            quantity = 0,
        };
    }
}