using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Block", menuName = "SoilGameObjects", order = 0)]
public class SoilObject : ScriptableObject
{
    public string stageName;       // Name of the soil stage (e.g., Dirt, Tilled Dirt)
    public Sprite stageSprite;     // Sprite for this stage (could be your sliced sprite from the sheet)
    public TileBase tilePrefab;    // Optional: if you want to link a tilePrefab directly
}