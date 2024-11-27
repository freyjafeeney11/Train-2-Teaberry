using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryItem : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image borderImage;
    public event Action<InventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;
    private bool empty = true;

    public void Awake()
    {
        ResetData();
        Deselect();
    }

    // Hide image and set bool to true if reset
    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;

    }
    public void Deselect()
    {
        borderImage.enabled = false;
    }
    public void SetData(Sprite sprite, int quantity)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantityText.text = quantity + "";
        empty = false;
    }
    public void Select()
    {
        borderImage.enabled = true;
    }
    public void OnBeginDrag()
    {
        if (empty)
        {
            return;
        }
        OnItemBeginDrag?.Invoke(this);
    }
    public void OnDrop()
    {
        OnItemDroppedOn?.Invoke(this);
    }
    public void OnEndDrag()
    {
        OnItemEndDrag?.Invoke(this);
    }
    public void OnPointerClick(BaseEventData data)
    {
        if (empty)
        {
            return;
        }
        PointerEventData pointerData = (PointerEventData)data;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }
}
