using UnityEngine;

[CreateAssetMenu(fileName = "Harvest Moon", menuName = "Harvest Moon/Create new item")]
public class Item : ScriptableObject {

    [SerializeField] private string name;
    [SerializeField] private Sprite sprite;
    [SerializeField] private ItemAction itemAction;

    public string Name => name;
    public Sprite Sprite => sprite;
    public ItemAction ItemAction => itemAction;
}
