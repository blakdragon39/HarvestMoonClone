using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour {
    
    [SerializeField] private float moveSpeed;
    
    private CropTiles cropTiles;
    private Rigidbody2D rigidBody;
    private PlayerAnimator animator;
    private Inventory inventory;

    private Vector2 input;
    private Vector2 lastInput;

    private void Awake() {
        cropTiles = FindObjectOfType<CropTiles>(); // todo might not exist in other scenes 
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<PlayerAnimator>();
        inventory = GetComponent<Inventory>();
    }

    private void Update() {
        if (
            Input.GetMouseButtonDown(0) &&
            inventory.SelectedItem != null && 
            inventory.SelectedItem.ItemAction != ItemAction.None
        ) {
            StartCoroutine(animator.StartItemAction(inventory.SelectedItem.ItemAction));
            HandleItemAction();
        }

        if (!animator.CanMove) return;
        
        UpdateInput();
        UpdateAnimation();
        Move();
        
        HighlightCropTiles();
    }

    private void UpdateInput() {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input != Vector2.zero) {
            lastInput.x = input.x;
            lastInput.y = input.y;
        }
    }

    private void UpdateAnimation() {
        animator.MoveX = lastInput.x;
        animator.MoveY = lastInput.y;
        animator.IsMoving = input != Vector2.zero;
    }

    private void Move() {
        rigidBody.MovePosition(transform.position.ToVector2() + input * moveSpeed * Time.deltaTime);
    }

    private void HandleItemAction() {
        switch (inventory.SelectedItem.ItemAction) {
            case ItemAction.PlantSeed:
                cropTiles.PlantSeed();
                break;
            case ItemAction.Hoe:
                cropTiles.HoeTile();
                break;
            case ItemAction.WateringCan:
                cropTiles.WaterTile();
                break;
        }
    }

    private void HighlightCropTiles() {
        if (inventory.SelectedItem != null && CropHighlights.actions.Contains(inventory.SelectedItem.ItemAction)) {
            cropTiles.SetTileHighlighted(GetFacingTilePos(), true);
        } else {
            cropTiles.UnhighlightTiles();
        }
    }

    private Vector3 GetFacingTilePos() {
        return transform.position + (animator.GetFacingDirection().FacingTileVector());
    }
}

public static class Vector3Ext {
    public static Vector2 ToVector2(this Vector3 vector3) {
        return new Vector2(vector3.x, vector3.y);
    }
}