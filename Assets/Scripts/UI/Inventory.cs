using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    
    private InventorySlot selectedSlot;

    [SerializeField] private List<InventorySlot> slots;
    [SerializeField] private List<Item> items;

    private void Start() {
        for (int i = 0; i < slots.Count && i < items.Count; i += 1) {
            slots[i].SlotImage.sprite = items[i].UnselectedSprite;
        }
    }
}