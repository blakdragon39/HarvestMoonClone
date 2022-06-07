using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [SerializeField] private List<Sprite> sprites;

    private SpriteAnimator animator;

    public void Start() {
        animator = new SpriteAnimator(sprites, GetComponent<SpriteRenderer>());
        animator.Start();
    }

    public IEnumerator OpenDoor() {
        yield return animator.StartAsCoroutine();
    }
}