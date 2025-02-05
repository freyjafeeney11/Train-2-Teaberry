using UnityEngine;

public class WaterBucket : MonoBehaviour
{
    public Transform holdBucket;
    public LayerMask pickUpMask;
    public Vector3 Direction { get; set; }
    private GameObject itemHolding;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (itemHolding) // Releasing the item
            {
                itemHolding.transform.position = transform.position + Direction; // Set to drop position
                itemHolding.transform.parent = null; // Unparent the item

                // Enable physics if the item has a Rigidbody2D
                Rigidbody2D rb = itemHolding.GetComponent<Rigidbody2D>();
                if (rb != null)
                    rb.simulated = true;

                // Optionally apply a force to make the item drop with some momentum
                rb?.AddForce(Direction.normalized * 2f, ForceMode2D.Impulse); // Adjust force as needed

                itemHolding = null;
            }
            else // Picking up the item
            {
                // Find the object within pickup range
                Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + Direction, .4f, pickUpMask);
                if (pickUpItem) // If an item is found
                {
                    itemHolding = pickUpItem.gameObject;
                    itemHolding.transform.position = holdBucket.position; // Position it in the held bucket's position
                    itemHolding.transform.parent = transform; // Parent the object to the player

                    // Disable physics forces while the item is held but still allow collisions
                    Rigidbody2D rb = itemHolding.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.linearVelocity = Vector2.zero;  // Stop any movement caused by forces
                        rb.angularVelocity = 0;      // Stop any rotation
                        rb.gravityScale = 0;         // Optionally disable gravity
                    }
                }
            }
        }

        // If we are holding an item, update its position to follow the player
        if (itemHolding != null)
        {
            itemHolding.transform.position = holdBucket.position;
        }
    }

    // Debug to visualize the pickup range (optional)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + Direction, 0.4f);
    }
}
