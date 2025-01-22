using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeBook", menuName = "Game/RecipeBook")]
public class RecipeBook : ScriptableObject
{
    [System.Serializable]
    public class Recipe
    {
        public string potionName;
        public List<string> ingredients;
    }

    public List<Recipe> recipes = new List<Recipe>();

    public string CheckRecipe(List<string> ingredients)
    {
        foreach (Recipe recipe in recipes)
        {
            if (IsMatch(recipe.ingredients, ingredients))
            {
                return recipe.potionName;
            }
        }
        return null; // No matching recipe found
    }

private bool IsMatch(List<string> recipeIngredients, List<string> cauldronIngredients)
{
    // Ensure both lists are not empty and have the same size before proceeding
    if (recipeIngredients == null || cauldronIngredients == null || recipeIngredients.Count != cauldronIngredients.Count)
    {
        return false;  // If either list is empty or sizes don't match, return false
    }

    // Compare the lists using HashSet to account for unordered matching
    HashSet<string> recipeSet = new HashSet<string>(recipeIngredients);
    HashSet<string> cauldronSet = new HashSet<string>(cauldronIngredients);

    return recipeSet.SetEquals(cauldronSet);
}


}
