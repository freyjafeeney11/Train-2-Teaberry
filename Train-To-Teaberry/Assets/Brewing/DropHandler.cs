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

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;

        if (droppedObject != null)
        {
            Ingredient ingredient = droppedObject.GetComponent<Ingredient>();

            if (ingredient != null)
            {
                string ingredientName = ingredient.ingredientName;

                // Remove ingredient from inventory
                inventoryController.RemoveItem(ingredientName);

                // Attach it to the cauldron visually
                droppedObject.transform.SetParent(transform);
                droppedObject.transform.position = transform.position; // Center on cauldron

                // Add to the ingredient list for brewing
                if (!ingredients.Contains(ingredientName))
                {
                    ingredients.Add(ingredientName);
                    ingredientObjects.Add(droppedObject);
                    Debug.Log("Added ingredient: " + ingredientName);
                }
            }
            else
            {
                Debug.LogError("Dropped object does not have an Ingredient component.");
            }
        }
    }

    public void BrewPotion()
    {
        Debug.Log("Ingredients in cauldron: " + string.Join(", ", ingredients));

        string brewedPotion = recipeBook.CheckRecipe(ingredients);

        if (!string.IsNullOrEmpty(brewedPotion))
        {
            Debug.Log("You brewed: " + brewedPotion);

            ClearIngredientObjects();
            ingredients.Clear();  

            potionDisplay.SetActive(true);
            potionImage.sprite = GetPotionSprite(brewedPotion);
            potionText.text = "You brewed a " + brewedPotion + "!";

            GameObject potionPrefab = GetPotionPrefab(brewedPotion);
            if (potionPrefab != null)
            {
                inventoryController.AddItem(potionPrefab);
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

    private Sprite GetPotionSprite(string potionName)
    {
        return Resources.Load<Sprite>($"Sprites/{potionName}");
    }

    private GameObject GetPotionPrefab(string potionName)
    {
        return Resources.Load<GameObject>($"PotionPrefabs/{potionName}Prefab");
    }

    private void ClosePotionDisplay()
    {
        potionDisplay.SetActive(false);
    }
}
