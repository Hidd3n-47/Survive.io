using System.Collections.Generic;
using UnityEngine;

public static class WallGen
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, DungeonTilemap tilemap, HashSet<Vector2> spawnPoints)
    {
        HashSet<Vector2Int> walls = new();
        HashSet<Vector2Int> tiles = new();
        HashSet<Vector2> spawns = new();
        Vector2Int[] directions = Direction2d.cardinalAndDiagonalDirections;
        Vector2Int[] cardDirections = Direction2d.cardinalDirections;

        foreach(Vector2Int floor in floorPositions)
        {
            foreach(Vector2Int direction in directions)
            {
                Vector2Int position = floor + direction;
                if(!floorPositions.Contains(position))
                {
                    int adjFloors = 0;
                    foreach (Vector2Int d in cardDirections)
                    {
                        if (floorPositions.Contains(position + d))
                        {
                            adjFloors++;
                        }
                    }

                    if(adjFloors >= 3)
                    {
                        tiles.Add(position);

                        float rand = Random.value;

                        if (rand < 0.3f)
                        {
                            spawns.Add(floor + Vector2.one * 0.5f);
                            DebugDraw.DrawBox(new BoundsInt((Vector3Int)(floor), Vector3Int.one), Color.yellow);
                        }
                    }
                    else
                    {
                        walls.Add(position);
                    }
                }
            }
        }
        tiles.UnionWith(floorPositions);
        spawnPoints.UnionWith(spawns);

        tilemap.PaintFloorTiles(tiles);
        tilemap.PaintWallTiles(walls);
    }
}
