using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator {
    
    private List<Sprite> frames;
    private SpriteRenderer spriteRenderer;
    private bool flipSprites;
    private float frameRate;

    private int currentFrame;
    private float timer;

    public List<Sprite> Frames => frames;
    
    public SpriteAnimator(
        List<Sprite> frames, 
        SpriteRenderer spriteRenderer, 
        bool flipSprites = false, 
        float frameRate = 0.16f
    ) {
        this.frames = frames;
        this.spriteRenderer = spriteRenderer;
        this.flipSprites = flipSprites;
        this.frameRate = frameRate;
    }

    public void Start() {
        currentFrame = 0;
        timer = 0;
        spriteRenderer.flipX = flipSprites;
        spriteRenderer.sprite = frames[currentFrame];
    }

    public void AdvanceFrame() {
        timer += Time.deltaTime;
        if (timer > frameRate) {
            currentFrame += 1;
            if (currentFrame == frames.Count) currentFrame = 0;
            
            spriteRenderer.sprite = frames[currentFrame];
            timer -= frameRate;
        }
    }
}