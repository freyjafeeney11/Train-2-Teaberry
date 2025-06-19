using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventoryDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject worldIngredientPrefab; // assign in inspector
    public RectTransform inventoryPanel;     // assign in inspector

    private Camera mainCamera;
    private GameObject spawnedWorldObject;
    private bool draggedOut = false;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggedOut = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!draggedOut && !RectTransformUtility.RectangleContainsScreenPoint(inventoryPanel, eventData.position))
        {
            // Drag has exited the UI panel — spawn world object
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(eventData.position);
            worldPos.z = 0;

            spawnedWorldObject = Instantiate(worldIngredientPrefab, worldPos, Quaternion.identity);
            var rb = spawnedWorldObject.GetComponent<Rigidbody2D>();
            if (rb) rb.isKinematic = true;

            draggedOut = true;
        }

        if (draggedOut && spawnedWorldObject != null)
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(eventData.position);
            worldPos.z = 0;
            spawnedWorldObject.transform.position = worldPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggedOut && spawnedWorldObject != null)
        {
            var rb = spawnedWorldObject.GetComponent<Rigidbody2D>();
            if (rb) rb.isKinematic = false;

            var col = spawnedWorldObject.GetComponent<Collider2D>();
            if (col) col.enabled = true;
        }
    }
}
