using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour {
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private CropTiles cropTiles;
    
    private PlayerAnimator animator;
    private Inventory inventory;

    private Vector2 input;
    private Vector2 lastInput;

    private void Awake() {
        animator = GetComponent<PlayerAnimator>();
        inventory = GetComponent<Inventory>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z) && inventory.SelectedItem.ItemAction != ItemAction.None) {
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
        transform.position += new Vector3(input.x * Time.deltaTime * moveSpeed, input.y * Time.deltaTime * moveSpeed);
    }

    private void HandleItemAction() {
        if (inventory.SelectedItem.ItemAction == ItemAction.PlantSeed) {
            cropTiles.PlantSeed(GetFacingTilePos());
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