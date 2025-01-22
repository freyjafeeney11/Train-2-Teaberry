using UnityEngine;
using UnityEngine.UI;  // Needed for UI Button
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DropHandler : MonoBehaviour, IDropHandler
{
    private List<string> ingredients = new List<string>();
    public RecipeBook recipeBook;  // Reference to the RecipeBook ScriptableObject
    public GameObject potionDisplay;  // Reference to the GameObject displaying the potion name and sprite
    public Image potionImage;         // Reference to the UI Image component where the sprite will appear
    public Text potionText;           // Reference to the UI Text component where the potion name will appea
    public Button brewButton;  // Reference to the Brew Button

    private void Start()
    {
        // Ensure the brewButton is assigned in the inspector
        if (brewButton != null)
        {
            brewButton.onClick.AddListener(BrewPotion);  // Listen for button click
        }
        else
        {
            Debug.LogError("Brew Button not assigned!");
        }

        // Ensure the cauldron has a collider
        if (GetComponent<Collider2D>() == null && GetComponent<Collider>() == null)
        {
            Debug.LogError("Cauldron does not have a Collider! Please add one.");
        }
    }

    public void BrewPotion()
    {

    // Log the ingredients in the cauldron
    Debug.Log("Ingredients in cauldron: " + string.Join(", ", ingredients));  // Log the ingredients list


        // Check if the ingredients match any recipe
        string brewedPotion = recipeBook.CheckRecipe(ingredients);

        if (!string.IsNullOrEmpty(brewedPotion))
        {
            Debug.Log("You brewed: " + brewedPotion);
            ingredients.Clear();  // Clear ingredients after a successful brew

            // Show the potion screen with the sprite and name
            potionDisplay.SetActive(true);  // Activate the potion display screen

            // Find and set the correct sprite for the brewed potion
            Sprite potionSprite = GetPotionSprite(brewedPotion);
            potionImage.sprite = potionSprite;

            // Display the potion name on the screen
            potionText.text = "Potion: " + brewedPotion;
        }
        else
        {
            Debug.Log("No potion brewed. Keep trying!");
        }
    }

    private Sprite GetPotionSprite(string potionName)
    {
        // Here, you can return different sprites based on the potion name
        // Example: you can use a switch-case, dictionary, or other logic to find the correct sprite

        switch (potionName)
        {
            case "Fart Potion":
                return Resources.Load<Sprite>("Fart Potion");  // Load the sprite from the Resources folder
            case "Health Potion":
                return Resources.Load<Sprite>("Health Potion");
            // Add more cases as needed
            default:
                return null;  // Return null if no matching potion is found
        }
    }
public void OnDrop(PointerEventData eventData)
{
    GameObject droppedObject = eventData.pointerDrag;

    // Check if the dropped object is valid
    if (droppedObject != null)
    {
        // Try to get the Ingredient component from the dropped object
        Ingredient ingredient = droppedObject.GetComponent<Ingredient>();

        if (ingredient != null)
        {
            // Add the ingredient name to the ingredients list
            ingredients.Add(ingredient.ingredientName);  // Assuming 'ingredientName' is a string in the Ingredient script
            Debug.Log("Added ingredient: " + ingredient.ingredientName);
        }
        else
        {
            Debug.LogError("Dropped object does not have an Ingredient component.");
        }
    }
}

}