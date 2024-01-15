using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonTilemap : MonoBehaviour
{
    [SerializeField]
    private Tilemap m_floorTilemap;
    [SerializeField]
    private Tilemap m_wallTilemap;

    [SerializeField]
    private TileBase m_floorTile; 
    [SerializeField]
    private TileBase m_floorTileTwo; 
    [SerializeField]
    private TileBase m_floorTileThree; 
    [SerializeField]
    private TileBase m_wallTile;
    [SerializeField]
    private TileBase m_wallTileTwo;
    [SerializeField]
    private TileBase m_wallTileThree;

    // Floor Percentages.
    [SerializeField]
    private float m_floorPercent = 0.7f;
    [SerializeField]
    private float m_floorTwoPercent = 0.2f;
    [SerializeField]
    private float m_floorThreePercent = 0.1f;

    // Wa;; Percentages.
    [SerializeField]
    private float m_wallPercent = 0.7f;
    [SerializeField]
    private float m_wallTwoPercent = 0.2f;
    [SerializeField]
    private float m_wallThreePercent = 0.1f;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        foreach (Vector2Int floor in floorPositions)
        {
            float rand = Random.value;


            TileBase tile;
            if(rand < m_floorThreePercent)
            {
                tile = m_floorTileThree;
            } 
            else if (rand < m_floorTwoPercent)
            {
                tile = m_floorTileTwo;
            }
            else
            {
                tile = m_floorTile;
            }    

            Vector3Int tilePosition = m_floorTilemap.WorldToCell((Vector3Int)floor);
            m_floorTilemap.SetTile(tilePosition, tile);
        }
    }

    public void PaintWallTiles(IEnumerable<Vector2Int> wallPositions)
    {
        foreach (Vector2Int floor in wallPositions)
        {
            float rand = Random.value;


            TileBase tile;
            if (rand < m_wallThreePercent)
            {
                tile = m_wallTileThree;
            }
            else if (rand < m_wallTwoPercent)
            {
                tile = m_wallTileTwo;
            }
            else
            {
                tile = m_wallTile;
            }

            Vector3Int tilePosition = m_wallTilemap.WorldToCell((Vector3Int)floor);
            m_wallTilemap.SetTile(tilePosition, tile);
        }
    }
}
