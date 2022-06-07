using System.Collections;
using DG.Tweening;
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
        cropTiles = FindObjectOfType<CropTiles>();
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

    public IEnumerator MoveToPosAndWalk(Vector3 pos, Vector2 moveDirection) {
        enabled = false;

        var result = pos + moveDirection.ToVector3();
        transform.position = pos;
        input = moveDirection;
        yield return transform.DOMove(result, .5f).WaitForCompletion();
        input = Vector2.zero;

        enabled = true;
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
        rigidBody.MovePosition((Vector2) transform.position + input * moveSpeed * Time.deltaTime);
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
