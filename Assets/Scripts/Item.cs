using UnityEngine;

[CreateAssetMenu(fileName = "Harvest Moon", menuName = "Harvest Moon/Create new item")]
public class Item : ScriptableObject {

    [SerializeField] private string name;
    [SerializeField] private Sprite unselectedSprite;
    [SerializeField] private Sprite selectedSprite;

    public string Name => name;
    public Sprite UnselectedSprite => unselectedSprite;
    public Sprite SelectedSprite => selectedSprite;
}
