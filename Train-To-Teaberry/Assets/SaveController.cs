using System.IO;
using UnityEngine;
using Unity.Cinemachine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private InventoryController inventoryController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        inventoryController = FindObjectOfType<InventoryController>();
        LoadGame();
    }
    public void SaveGame()
    {
        // GameObject player = GameObject.FindGameObjectWithTag("Player");
        // if (player == null)
        // {
        //     Debug.LogError("SaveGame Error: Player GameObject not found! Make sure the Player is tagged as 'Player'.");
        //     return;
        // }

        // CinemachineConfiner confiner = FindObjectOfType<CinemachineConfiner>();
        // if (confiner == null || confiner.m_BoundingShape2D == null)
        // {
        //     Debug.LogError("SaveGame Error: CinemachineConfiner or its BoundingShape2D is missing!");
        //     return;
        // }

        SaveData saveData = new SaveData
        {
            // playerPosition = player.transform.position,
            // mapBoundry = confiner.m_BoundingShape2D.gameObject.name,
            inventorySaveData = inventoryController.GetInventoryItems()
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
        Debug.Log("Game saved! JSON written to: " + saveLocation);
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            string json = File.ReadAllText(saveLocation);
            Debug.Log("Loaded save data: " + json);

            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            // GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            // FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D = GameObject.Find(saveData.mapBoundry).GetComponent<PolygonCollider2D>();

            inventoryController.SetInventoryItems(saveData.inventorySaveData);
        }
        else
        {
            Debug.Log("Save file not found, creating new save.");
            SaveGame();
        }
    }
}
