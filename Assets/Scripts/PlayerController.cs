using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour {

    private Animator animator;

    private readonly int moveXId = Animator.StringToHash("moveX");
    private readonly int moveYId = Animator.StringToHash("moveY");
    private readonly int isWalkingId = Animator.StringToHash("isWalking");

    private Vector2 input;
    private Vector2 lastInput;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        UpdateInput();

        if (input == Vector2.zero) {
            animator.Rebind();
        }

        animator.SetFloat(moveXId, lastInput.x);
        animator.SetFloat(moveYId, lastInput.y);
        animator.SetBool(isWalkingId, input != Vector2.zero);
    }

    private void UpdateInput() {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input != Vector2.zero) {
            lastInput.x = input.x;
            lastInput.y = input.y;
        }
    }
}