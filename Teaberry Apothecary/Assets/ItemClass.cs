using System.Collections;
using UnityEngine;
public abstract class ItemClass {
    [Header("Item")]
    public string itemName;
    public Sprite itemSprite;

    public abstract ItemClass GetItem();
    public abstract ToolClass GetTool();
    public abstract ConsumableClass GetConsumable();
}
