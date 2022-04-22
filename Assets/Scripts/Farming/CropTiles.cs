using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTiles : MonoBehaviour {
    
    [SerializeField] private Tilemap cropTiles;
    [SerializeField] private Tilemap cropsWatered;
    [SerializeField] private Tilemap cropsPlanted;
    [SerializeField] private Tilemap cropHighlights;
    
    [SerializeField] private Tile highlightTile;
    [SerializeField] private Tile plantedTile;
    [SerializeField] private Tile seedlingTile;
    [SerializeField] private Tile wateredTile;

    [SerializeField] private float growthTime;
    
    private Grid grid;

    private Vector3Int highlightedTilePos;
    private float growthTimer;

    private List<Vector3Int> wateredTiles = new List<Vector3Int>();
    private List<Vector3Int> plantedTiles = new List<Vector3Int>();

    private void Awake() {
        grid = GetComponent<Grid>();
    }

    private void Update() {
        growthTimer += Time.deltaTime;

        if (growthTimer > growthTime) {
            ProgressCrops();
            growthTimer -= growthTime;
        }
    }

    public void PlantSeed() {
        if (cropTiles.GetTile(highlightedTilePos) != null && !plantedTiles.Contains(highlightedTilePos)) {
            cropsPlanted.SetTile(highlightedTilePos, plantedTile);
            plantedTiles.Add(highlightedTilePos);
        }
    }

    public void WaterTile() {
        if (cropTiles.GetTile(highlightedTilePos) != null && !wateredTiles.Contains(highlightedTilePos)) {
            cropsWatered.SetTile(highlightedTilePos, wateredTile);
            wateredTiles.Add(highlightedTilePos);
        }
    }

    public void SetTileHighlighted(Vector3 position, bool highlighted) {
        var tilePos = position.GetCellPosition(grid);

        if (tilePos != highlightedTilePos) {
            cropHighlights.SetTile(highlightedTilePos, null);
            
            if (cropTiles.GetTile(tilePos) != null) {
                cropHighlights.SetTile(tilePos, highlighted ? highlightTile : null);
            }

            highlightedTilePos = tilePos;
        }
    }

    public void UnhighlightTiles() {
        cropHighlights.SetTile(highlightedTilePos, null);
        highlightedTilePos = Vector3Int.zero;
    }

    private void ProgressCrops() {
        var growthTiles = wateredTiles.Intersect(plantedTiles);
        
        foreach (var tilePos in growthTiles) {
            cropsPlanted.SetTile(tilePos, seedlingTile);
            cropsWatered.SetTile(tilePos, null);
            wateredTiles = new List<Vector3Int>();
        }
    }
}

static class CropTilesExtensions {
    public static Vector3Int GetCellPosition(this Vector3 position, Grid grid) {
        return grid.WorldToCell(position);
    }
}