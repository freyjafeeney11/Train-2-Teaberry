using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DropHandler : MonoBehaviour, IDropHandler
{
    private List<string> ingredients = new List<string>();
    private List<GameObject> ingredientObjects = new List<GameObject>();  

    public RecipeBook recipeBook;  
    public GameObject potionDisplay; 
    public Image potionImage;        
    public Text potionText;           
    public Button brewButton;  

    // Reference to InventoryController
    public InventoryController inventoryController;

    private void Start()
    {
        if (brewButton != null)
        {
            brewButton.onClick.AddListener(BrewPotion);
        }
        else
        {
            Debug.LogError("Brew Button not assigned!");
        }

        if (potionDisplay != null)
        {
            potionDisplay.GetComponent<Button>().onClick.AddListener(ClosePotionDisplay);
        }
        else
        {
            Debug.LogError("Potion Display not assigned!");
        }

        if (GetComponent<Collider2D>() == null && GetComponent<Collider>() == null)
        {
            Debug.LogError("Cauldron does not have a Collider! Please add one.");
        }
    }

    public void BrewPotion()
    {
        Debug.Log("Ingredients in cauldron: " + string.Join(", ", ingredients));

        string brewedPotion = recipeBook.CheckRecipe(ingredients);

        if (!string.IsNullOrEmpty(brewedPotion))
        {
            Debug.Log("You brewed: " + brewedPotion);

            // Remove ingredient GameObjects from the scene
            ClearIngredientObjects();

            // Clear ingredient names from the list
            ingredients.Clear();  

            // Show the potion display
            potionDisplay.SetActive(true);

            // Set the correct sprite for the brewed potion
            Sprite potionSprite = GetPotionSprite(brewedPotion);
            potionImage.sprite = potionSprite;

            // Display the potion name
            potionText.text = "You brewed a " + brewedPotion + "!";

            // Now add the brewed potion to the inventory
            GameObject potionPrefab = GetPotionPrefab(brewedPotion);
            if (potionPrefab != null)
            {
                Debug.Log("Prefab for brewed potion found: " + potionPrefab.name);
                inventoryController.AddItem(potionPrefab); // Add potion to inventory
            }
            else
            {
                Debug.LogError("Prefab for brewed potion not found.");
            }
        }
        else
        {
            Debug.Log("No potion brewed. Keep trying!");
        }
    }

    private void ClearIngredientObjects()
    {
        foreach (GameObject ingredient in ingredientObjects)
        {
            if (ingredient != null)
            {
                Destroy(ingredient);
            }
        }
        ingredientObjects.Clear();  
    }

public void OnDrop(PointerEventData eventData)
{
    GameObject droppedObject = eventData.pointerDrag;

    if (droppedObject != null)
    {
        Ingredient ingredient = droppedObject.GetComponent<Ingredient>();

        if (ingredient != null)
        {
            droppedObject.transform.SetParent(transform); // Attach it to the cauldron

            // Remove the ingredient from the inventory by passing its name
            bool removed = inventoryController.RemoveItem(ingredient.ingredientName);  

            if (removed)
            {
                Debug.Log("Removed " + ingredient.ingredientName + " from inventory.");
            }
            else
            {
                Debug.LogWarning("Failed to remove " + ingredient.ingredientName + " from inventory.");
            }
        }
        else
        {
            Debug.LogError("Dropped object does not have an Ingredient component.");
        }
    }
}




    // Check if an ingredient actually enters the cauldron before adding it to the list
    private void OnTriggerEnter2D(Collider2D other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null && !ingredients.Contains(ingredient.ingredientName))
        {
            ingredients.Add(ingredient.ingredientName);
            ingredientObjects.Add(other.gameObject);
            Debug.Log("Added ingredient: " + ingredient.ingredientName);
        }
    }

    private Sprite GetPotionSprite(string potionName)
    {
        switch (potionName)
        {
            case "Fart Potion":
                return Resources.Load<Sprite>("Fart Potion");  
            case "Health Potion":
                return Resources.Load<Sprite>("Health Potion");
            default:
                return null;  
        }
    }

    private GameObject GetPotionPrefab(string potionName)
    {
        switch (potionName)
        {
            case "Fart Potion":
                Debug.Log("Looking for Fart Potion prefab...");
                return Resources.Load<GameObject>("PotionPrefabs/FartPotionPrefab");  // Replace with actual path to prefab
            case "Health Potion":
                Debug.Log("Looking for Health Potion prefab...");
                return Resources.Load<GameObject>("PotionPrefabs/HealthPotionPrefab");
            default:
                return null;
        }
    }

    private void ClosePotionDisplay()
    {
        potionDisplay.SetActive(false);
    }
}