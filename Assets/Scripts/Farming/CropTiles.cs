using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTiles : MonoBehaviour {
    
    [SerializeField] private Tilemap cropTiles;
    [SerializeField] private Tilemap cropsPlanted;
    [SerializeField] private Tilemap cropHighlights;
    
    [SerializeField] private Tile highlightTile;
    [SerializeField] private Tile plantedTile;
    
    private Grid grid;

    private Vector3Int prevTilePos;

    private void Awake() {
        grid = GetComponent<Grid>();
    }

    public void PlantSeed(Vector3 position) {
        var tilePos = position.GetCellPosition(grid);
        if (cropTiles.GetTile(tilePos) != null) {
            cropsPlanted.SetTile(tilePos, plantedTile);
        }
    }

    public void SetTileHighlighted(Vector3 position, bool highlighted) {
        var tilePos = position.GetCellPosition(grid);

        if (tilePos != prevTilePos) {
            cropHighlights.SetTile(prevTilePos, null);
            
            if (cropTiles.GetTile(tilePos) != null) {
                cropHighlights.SetTile(tilePos, highlighted ? highlightTile : null);
            }

            prevTilePos = tilePos;
        }
    }

    public void UnhighlightTiles() {
        cropHighlights.SetTile(prevTilePos, null);
        prevTilePos = Vector3Int.zero;
    }
}

static class CropTilesExtensions {
    public static Vector3Int GetCellPosition(this Vector3 position, Grid grid) {
        return grid.WorldToCell(position);
    }
}