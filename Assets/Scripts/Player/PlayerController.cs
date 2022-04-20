using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour {
    
    [SerializeField] private float moveSpeed;
    
    private PlayerAnimator animator;

    private Vector2 input;
    private Vector2 lastInput;

    private void Awake() {
        animator = GetComponent<PlayerAnimator>();
    }

    private void Update() {
        UpdateInput();
        UpdateAnimation();
        Move();
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
        if (input == Vector2.zero) {
            animator.ItemAction = ItemAction.None;
        }

        animator.MoveX = lastInput.x;
        animator.MoveY = lastInput.y;
        animator.IsMoving = input != Vector2.zero;
    }

    private void Move() {
        transform.position += new Vector3(input.x * Time.deltaTime * moveSpeed, input.y * Time.deltaTime * moveSpeed);
    }
}