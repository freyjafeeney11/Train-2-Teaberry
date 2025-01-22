using UnityEngine;

public class IngredientCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object it collides with is the cauldron
        if (other.gameObject.CompareTag("Cauldron"))
        {
            // Destroy the ingredient (make it disappear)
            //Destroy(gameObject);
        }
    }
}
