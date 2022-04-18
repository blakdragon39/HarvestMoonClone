using UnityEngine;

public class Inventory : MonoBehaviour {
    
    private InventorySlot selectedSlot;
    
    [SerializeField] private InventorySlot slot1;
    [SerializeField] private InventorySlot slot2;
    [SerializeField] private InventorySlot slot3;
    [SerializeField] private InventorySlot slot4;
    [SerializeField] private InventorySlot slot5;
    [SerializeField] private InventorySlot slot6;
    [SerializeField] private InventorySlot slot7;
    [SerializeField] private InventorySlot slot8;
    [SerializeField] private InventorySlot slot9;

    [SerializeField] private Item item1;
    [SerializeField] private Item item2;
    [SerializeField] private Item item3;
    [SerializeField] private Item item4;
    [SerializeField] private Item item5;
    [SerializeField] private Item item6;
    [SerializeField] private Item item7;
    [SerializeField] private Item item8;
    [SerializeField] private Item item9;

    private void Start() {
        slot1.SlotImage.sprite = item1.UnselectedSprite;
        slot2.SlotImage.sprite = item2.UnselectedSprite;
        slot3.SlotImage.sprite = item3.UnselectedSprite;
        slot4.SlotImage.sprite = item4.UnselectedSprite;
        slot5.SlotImage.sprite = item5.UnselectedSprite;
        slot6.SlotImage.sprite = item6.UnselectedSprite;
        slot7.SlotImage.sprite = item7.UnselectedSprite;
        slot8.SlotImage.sprite = item8.UnselectedSprite;
        slot9.SlotImage.sprite = item9.UnselectedSprite;
    }
}