using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTiles : MonoBehaviour {

    [SerializeField] private Tile highlightTile;
    [SerializeField] private Tilemap cropTileMap;
    [SerializeField] private Tilemap cropsHighlightTileMap;
    private Grid grid;

    private Vector3Int prevTilePos;

    private void Awake() {
        grid = GetComponent<Grid>();
    }

    public void SetTileHighlighted(Vector3 position, bool highlighted) {
        var tilePos = grid.WorldToCell(position);

        if (tilePos != prevTilePos) {
            cropsHighlightTileMap.SetTile(prevTilePos, null);
            
            if (cropTileMap.GetTile(tilePos) != null) {
                cropsHighlightTileMap.SetTile(tilePos, highlighted ? highlightTile : null);
            }

            prevTilePos = tilePos;
        }
    }
}