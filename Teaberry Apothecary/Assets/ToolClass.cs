using System.Collections;
using UnityEngine;
public abstract class ToolClass : ItemClass {
    [Header("Tool")]
    public ToolType toolType;
    public enum ToolType {
        clock,
        bottle,
        knife
    }
    public override ItemClass GetItem() { return this; }
    public override ToolClass GetTool() { return this; }
    public abstract ConsumableClass GetConsumable() { return null; }
}
