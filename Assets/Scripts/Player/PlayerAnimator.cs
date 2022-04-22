using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    
    #region Sprites
    [SerializeField] private List<Sprite> walkDownSprites;
    [SerializeField] private List<Sprite> walkUpSprites;
    [SerializeField] private List<Sprite> walkRightSprites;

    [SerializeField] private List<Sprite> swingAxeDownSprites;
    [SerializeField] private List<Sprite> swingAxeUpSprites;
    [SerializeField] private List<Sprite> swingAxeRightSprites;
    
    [SerializeField] private List<Sprite> swingHoeDownSprites;
    [SerializeField] private List<Sprite> swingHoeUpSprites;
    [SerializeField] private List<Sprite> swingHoeRightSprites;
    
    [SerializeField] private List<Sprite> swingHammerDownSprites;
    [SerializeField] private List<Sprite> swingHammerUpSprites;
    [SerializeField] private List<Sprite> swingHammerRightSprites;
    
    [SerializeField] private List<Sprite> swingScytheDownSprites;
    [SerializeField] private List<Sprite> swingScytheUpSprites;
    [SerializeField] private List<Sprite> swingScytheRightSprites;
    
    [SerializeField] private List<Sprite> wateringCanDownSprites;
    [SerializeField] private List<Sprite> wateringCanUpSprites;
    [SerializeField] private List<Sprite> wateringCanLeftSprites;

    [SerializeField] private List<Sprite> plantSeedSprites;
    #endregion 

    #region SpriteAnimators
    private Dictionary<FacingDirection, SpriteAnimator> walkAnims;
    private Dictionary<FacingDirection, SpriteAnimator> swingAxeAnims;
    private Dictionary<FacingDirection, SpriteAnimator> swingHoeAnims;
    private Dictionary<FacingDirection, SpriteAnimator> swingHammerAnims;
    private Dictionary<FacingDirection, SpriteAnimator> swingScytheAnims;
    private Dictionary<FacingDirection, SpriteAnimator> wateringCanAnims;
    private Dictionary<FacingDirection, SpriteAnimator> plantSeedAnims;

    private Dictionary<ItemAction, Dictionary<FacingDirection, SpriteAnimator>> anims;
    #endregion

    public float MoveX { get; set; }
    public float MoveY { get; set; }
    public bool IsMoving { get; set; }

    public bool CanMove => currentAnim == null || currentAnim.CanCancel;

    private SpriteRenderer renderer;

    private SpriteAnimator prevAnim;
    private SpriteAnimator currentAnim;

    private void Start() {
        renderer = GetComponent<SpriteRenderer>();
        
        #region Init SpriteAnimators

        walkAnims = new Dictionary<FacingDirection, SpriteAnimator> {
            {FacingDirection.Up, new SpriteAnimator(walkUpSprites, renderer)},
            {FacingDirection.Down, new SpriteAnimator(walkDownSprites, renderer)},
            {FacingDirection.Right, new SpriteAnimator(walkRightSprites, renderer)},
            {FacingDirection.Left, new SpriteAnimator(walkRightSprites, renderer, flipSprites: true)},
        };

        swingAxeAnims = new Dictionary<FacingDirection, SpriteAnimator> {
            {FacingDirection.Up, new SpriteAnimator(swingAxeUpSprites, renderer)},
            {FacingDirection.Down, new SpriteAnimator(swingAxeDownSprites, renderer)},
            {FacingDirection.Right, new SpriteAnimator(swingAxeRightSprites, renderer)},
            {FacingDirection.Left, new SpriteAnimator(swingAxeRightSprites, renderer, flipSprites: true)},
        };

        swingHammerAnims = new Dictionary<FacingDirection, SpriteAnimator> {
            {FacingDirection.Up, new SpriteAnimator(swingHammerUpSprites, renderer)},
            {FacingDirection.Down, new SpriteAnimator(swingHammerDownSprites, renderer)},
            {FacingDirection.Right, new SpriteAnimator(swingHammerRightSprites, renderer)},
            {FacingDirection.Left, new SpriteAnimator(swingHammerRightSprites, renderer, flipSprites: true)}
        };

        swingHoeAnims = new Dictionary<FacingDirection, SpriteAnimator> {
            {FacingDirection.Up, new SpriteAnimator(swingHoeUpSprites, renderer)},
            {FacingDirection.Down, new SpriteAnimator(swingHoeDownSprites, renderer)},
            {FacingDirection.Right, new SpriteAnimator(swingHoeRightSprites, renderer)},
            {FacingDirection.Left, new SpriteAnimator(swingHoeRightSprites, renderer, flipSprites: true)}
        };

        swingScytheAnims = new Dictionary<FacingDirection, SpriteAnimator> {
            {FacingDirection.Up, new SpriteAnimator(swingScytheUpSprites, renderer)},
            {FacingDirection.Down, new SpriteAnimator(swingScytheDownSprites, renderer)},
            {FacingDirection.Right, new SpriteAnimator(swingScytheRightSprites, renderer)},
            {FacingDirection.Left, new SpriteAnimator(swingScytheRightSprites, renderer, flipSprites: true)}
        };

        wateringCanAnims = new Dictionary<FacingDirection, SpriteAnimator> {
            {FacingDirection.Up, new SpriteAnimator(wateringCanUpSprites, renderer)},
            {FacingDirection.Down, new SpriteAnimator(wateringCanDownSprites, renderer)},
            {FacingDirection.Left, new SpriteAnimator(wateringCanLeftSprites, renderer)},
            {FacingDirection.Right, new SpriteAnimator(wateringCanLeftSprites, renderer, flipSprites: true)}
        };

        plantSeedAnims = new Dictionary<FacingDirection, SpriteAnimator> {
            {FacingDirection.Up, new SpriteAnimator(plantSeedSprites, renderer)},
            {FacingDirection.Down, new SpriteAnimator(plantSeedSprites, renderer)},
            {FacingDirection.Left, new SpriteAnimator(plantSeedSprites, renderer)},
            {FacingDirection.Right, new SpriteAnimator(plantSeedSprites, renderer)}
        };

        anims = new Dictionary<ItemAction, Dictionary<FacingDirection, SpriteAnimator>> {
            {ItemAction.None, walkAnims},
            {ItemAction.Axe, swingAxeAnims},
            {ItemAction.Hammer, swingHammerAnims},
            {ItemAction.Hoe, swingHoeAnims},
            {ItemAction.Scythe, swingScytheAnims},
            {ItemAction.WateringCan, wateringCanAnims},
            {ItemAction.PlantSeed, plantSeedAnims}
        };

        #endregion
    }

    private void Update() {
        if (!CanMove) return;
        
        prevAnim = currentAnim;
        var direction = GetFacingDirection();
        currentAnim = anims[ItemAction.None][direction];
        
        if (!currentAnim.CanCancel) return;
        
        if (prevAnim != currentAnim) {
            currentAnim.Start();
        }
        
        if (IsMoving) {
            currentAnim.AdvanceFrame();
        } else {
            currentAnim.Start();
        }
    }

    public IEnumerator StartItemAction(ItemAction action) {
        currentAnim = anims[action][GetFacingDirection()];
        yield return currentAnim.StartAsCoroutine();
    }

    public FacingDirection GetFacingDirection() {
        if (MoveX == 1) return FacingDirection.Right;
        if (MoveX == -1) return FacingDirection.Left;
        if (MoveY == 1) return FacingDirection.Up;
        return FacingDirection.Down; // MoveY == -1
    }
}
