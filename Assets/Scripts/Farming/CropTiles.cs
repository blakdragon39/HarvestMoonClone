using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTiles : MonoBehaviour {
    
    [SerializeField] private Tilemap ground;
    [SerializeField] private Tilemap cropsHoed;
    [SerializeField] private Tilemap cropsWatered;
    [SerializeField] private Tilemap cropsPlanted;
    [SerializeField] private Tilemap cropHighlighted;
    
    [SerializeField] private Tile highlightTile;
    [SerializeField] private RuleTile hoedTile;
    [SerializeField] private Tile plantedTile;
    [SerializeField] private Tile seedlingTile;
    [SerializeField] private Tile wateredTile;

    [SerializeField] private float growthTime;
    
    private Grid grid;

    private List<Vector3Int> hoedTiles = new List<Vector3Int>();
    private List<Vector3Int> wateredTiles = new List<Vector3Int>();
    private List<Vector3Int> plantedTiles = new List<Vector3Int>();
    
    private Vector3Int highlightedTilePos;
    private float growthTimer;

    private void Awake() {
        grid = GetComponentInParent<Grid>();
    }

    private void Update() {
        growthTimer += Time.deltaTime;
        
        if (growthTimer > growthTime) {
            ProgressCrops();
            growthTimer -= growthTime;
        }
    }

    public void HoeTile() {
        if (ground.GetTile(highlightedTilePos) != null && !hoedTiles.Contains(highlightedTilePos)) {
            for (int x = -1; x <= 1; x += 1) {
                for (int y = -1; y <= 1; y += 1) {
                    Vector3Int surroundingTile = new Vector3Int(highlightedTilePos.x + x, highlightedTilePos.y + y, highlightedTilePos.z);
                    cropsHoed.SetTile(surroundingTile, hoedTile);        
                }
            }
            
            hoedTiles.Add(highlightedTilePos);
        }
    }

    public void PlantSeed() {
        if (cropsHoed.GetTile(highlightedTilePos) != null && !plantedTiles.Contains(highlightedTilePos)) {
            cropsPlanted.SetTile(highlightedTilePos, plantedTile);
            plantedTiles.Add(highlightedTilePos);
        }
    }
    
    public void WaterTile() {
        if (cropsHoed.GetTile(highlightedTilePos) != null && !wateredTiles.Contains(highlightedTilePos)) {
            cropsWatered.SetTile(highlightedTilePos, wateredTile);
            wateredTiles.Add(highlightedTilePos);
        }
    }
    
    public void SetTileHighlighted(Vector3 position, bool highlighted) {
        var tilePos = position.GetCellPosition(grid);
        
        if (tilePos != highlightedTilePos) {
            cropHighlighted.SetTile(highlightedTilePos, null);
            
            if (ground.GetTile(tilePos) != null) {
                cropHighlighted.SetTile(tilePos, highlighted ? highlightTile : null);
            }
        
            highlightedTilePos = tilePos;
        }
    }
    
    public void UnhighlightTiles() {
        cropHighlighted.SetTile(highlightedTilePos, null);
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
        return grid.WorldToCell((Vector2) position);
    }
}