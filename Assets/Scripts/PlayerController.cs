using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour {

    private Animator animator;

    private readonly int moveXId = Animator.StringToHash("moveX");
    private readonly int moveYId = Animator.StringToHash("moveY");
    private readonly int isWalkingId = Animator.StringToHash("isWalking");

    private Vector2 lastInput;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void Update() {
        Vector2 newInput;
        newInput.x = Input.GetAxisRaw("Horizontal");
        newInput.y = Input.GetAxisRaw("Vertical");

        if (newInput.x != 0 || newInput.y != 0) {
            lastInput.x = newInput.x;
            lastInput.y = newInput.y;
        }

        if (newInput == Vector2.zero) {
            animator.Rebind();
            animator.Update(0f);
        }
        
        animator.SetFloat(moveXId, lastInput.x);
        animator.SetFloat(moveYId, lastInput.y);
        animator.SetBool(isWalkingId, newInput != Vector2.zero);
    }
}