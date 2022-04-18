using System;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public event Action<Item> OnItemClick;
    public Item Item => item;
    
    [SerializeField] private Image slotImage;
    private Item item;

    public void SetItem(Item item) {
        this.item = item;
        slotImage.sprite = item.UnselectedSprite;
    }

    public void SetSelected(bool selected) {
        slotImage.sprite = selected ? item.SelectedSprite : item.UnselectedSprite;
    }
    
    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            OnItemClick(item);
        }
    }
}