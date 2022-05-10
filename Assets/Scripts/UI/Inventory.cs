using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    
    [SerializeField] private List<InventorySlot> slots;
    [SerializeField] private List<Item> items;
    
    private Item selectedItem;

    public Item SelectedItem => selectedItem;

    private void Start() {
        for (int i = 0; i < slots.Count && i < items.Count; i += 1) {
            slots[i].SetItem(items[i]);
            slots[i].OnItemClick += OnItemSelected;
        }
    }

    private void Update() {
        var selectedItemIndex = items.IndexOf(selectedItem);
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        
        if (scroll > 0) selectedItemIndex -= 1;
        else if (scroll < 0) selectedItemIndex += 1;

        if (scroll != 0) {
            selectedItemIndex = Mathf.Clamp(selectedItemIndex, 0, items.Count - 1);
            OnItemSelected(items[selectedItemIndex]);
        }
    }

    private void OnItemSelected(Item item) {
        selectedItem = item;

        for (int i = 0; i < items.Count; i++) {
            slots[i].SetSelected(slots[i].Item == item);
        }
    }
}