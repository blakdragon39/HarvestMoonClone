using System;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public event Action<Item> OnItemClick;
    public Item Item => item;
    
    [SerializeField] private Image itemImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite unselectedBackgroundSprite;
    [SerializeField] private Sprite selectedBackgroundSprite;
    private Item item;

    public void SetItem(Item item) {
        this.item = item;
        itemImage.sprite = item.Sprite;
        backgroundImage.sprite = unselectedBackgroundSprite;
    }
    
    public void SetSelected(bool selected) {
        backgroundImage.sprite = selected ? selectedBackgroundSprite : unselectedBackgroundSprite;
    }
    
    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            OnItemClick(item);
        }
    }
}