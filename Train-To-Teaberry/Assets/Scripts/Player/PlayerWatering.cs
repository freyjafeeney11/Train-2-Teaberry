using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerWatering : MonoBehaviour
{
    public Tilemap soilTilemap; // The tilemap containing the soil
    public SoilObject[] soilStages; // Array of soil stages
    public LayerMask waterBucketMask; // Layer for the water bucket
    public float wateringTime = 1f; // Time to water soil

    private float wateringTimer = 0f;
    private bool isWatering = false;
    private Vector3Int currentTilePosition;

    // Track if the player is holding a water bucket
    private bool isHoldingWaterBucket = false;

    void Update()
    {
        // Detect the tile the player is currently near
        Vector3 worldPosition = transform.position;
        currentTilePosition = soilTilemap.WorldToCell(worldPosition);

        // Check if the player is holding the water bucket and update the watering action
        if (isHoldingWaterBucket)
        {
            // Start watering if the player is holding the water bucket
            if (isWatering && soilTilemap.HasTile(currentTilePosition))
            {
                wateringTimer += Time.deltaTime;

                if (wateringTimer >= wateringTime)
                {
                    ChangeSoilTile();
                    wateringTimer = 0f; // Reset the timer
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("WaterBucket"))
        {
            isWatering = true;
            isHoldingWaterBucket = true;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player exits the collision with the water bucket
        if (other.CompareTag("WaterBucket"))
        {
            isWatering = false;
            isHoldingWaterBucket = false; // Player is no longer holding the water bucket
        }
    }

    void ChangeSoilTile()
    {
        TileBase currentTile = soilTilemap.GetTile(currentTilePosition);

        if (currentTile != null)
        {
            Debug.Log($"Current tile: {currentTile.name}");
            for (int i = 0; i < soilStages.Length; i++)
            {
                if (currentTile == soilStages[i].tilePrefab)
                {
                    if (i + 1 < soilStages.Length)
                    {
                        // Change the tile to the next stage
                        soilTilemap.SetTile(currentTilePosition, soilStages[i + 1].tilePrefab);
                        Debug.Log($"Tile changed to: {soilStages[i + 1].tilePrefab.name}");
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.Log("No tile found at player's position");
        }
    }
}
