using System.Collections.Generic;

[System.Serializable]
public enum ItemAction {
    None, Axe, Hammer, Hoe, Scythe, WateringCan, PlantSeed
}

public static class CropHighlights {
    public static List<ItemAction> actions = new List<ItemAction> {
        ItemAction.Hoe,
        ItemAction.WateringCan, 
        ItemAction.PlantSeed
    };
}