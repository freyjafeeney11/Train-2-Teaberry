using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;
    public GameObject worldPrefab; // assign a physics mushroom prefab
    public RectTransform inventoryPanel; // assign in inspector
    private GameObject spawnedWorldObject;
    private bool draggedOut = false;
    private Camera mainCamera;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        mainCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
        draggedOut = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        // If pointer is outside the inventory panel, spawn a world object
        if (!draggedOut && !RectTransformUtility.RectangleContainsScreenPoint(inventoryPanel, eventData.position))
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(eventData.position);
            worldPos.z = 0f;
            spawnedWorldObject = Instantiate(worldPrefab, worldPos, Quaternion.identity);
            var rb = spawnedWorldObject.GetComponent<Rigidbody2D>();
            if (rb != null) rb.isKinematic = true;
            draggedOut = true;
        }

        // Move the world object with the mouse
        if (draggedOut && spawnedWorldObject != null)
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(eventData.position);
            worldPos.z = 0f;
            spawnedWorldObject.transform.position = worldPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        if (draggedOut)
        {
            if (spawnedWorldObject != null)
            {
                var rb = spawnedWorldObject.GetComponent<Rigidbody2D>();
                if (rb != null) rb.isKinematic = false;

                var col = spawnedWorldObject.GetComponent<Collider2D>();
                if (col != null) col.enabled = true;
            }

            // Optionally: remove item from UI inventory
            Destroy(gameObject); // or disable it instead
            return; // skip UI snap-back logic
        }

        // === Original UI Drop Logic ===
        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>();
        if (dropSlot == null && eventData.pointerEnter != null)
        {
            dropSlot = eventData.pointerEnter.GetComponentInParent<Slot>();
        }
        Slot originalSlot = originalParent.GetComponent<Slot>();

        if (dropSlot != null)
        {
            if (dropSlot.currentItem != null)
            {
                dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                originalSlot.currentItem = dropSlot.currentItem;
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                originalSlot.currentItem = null;
            }

            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
        }
        else
        {
            transform.SetParent(originalParent);
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
