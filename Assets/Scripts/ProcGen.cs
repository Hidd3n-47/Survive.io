using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class ProcGen : MonoBehaviour
{
    [SerializeField]
    private int m_minRoomWidth = 6;
    [SerializeField]
    private int m_minRoomHeight = 4;
    [SerializeField]
    private int m_mapWidth = 50;
    [SerializeField]
    private int m_mapHeight = 50;
    [SerializeField, Range(0, 5)]
    private int m_offsetBetweenRooms = 1;

    [SerializeField]
    private int m_rndWalkIterations = 30;
    [SerializeField]
    private int m_walkLength = 8;

    [SerializeField]
    private DungeonTilemap m_dungeonTilemap;

    List<Vector2Int> m_roomCenters = new();
    List<BoundsInt> m_rooms = new();
    List<HashSet<Vector2Int>> m_roomFloors = new();

    HashSet<Vector2> m_spawners = new();

    Vector2Int m_playerStart;
    Vector2Int m_furtherestFromPlayer;

    public void RunProcGen()
    {
        m_rooms = ProcGenAlgorithms.PartitionArea(new BoundsInt(Vector3Int.zero, new Vector3Int(m_mapWidth, m_mapHeight)), m_minRoomWidth, m_minRoomHeight);
        HashSet<Vector2Int> floor = CreateRandomRoomShape();

        foreach (BoundsInt room in m_rooms)
        {
            m_roomCenters.Add(Vector2Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms();
        floor.UnionWith(corridors);

        WallGen.CreateWalls(floor, m_dungeonTilemap, m_spawners);
    }

    public void Init()
    {
        GameObject.Find("EnemySpawner").GetComponent<SpawnEnemiesSurvival>().SetSpawns(m_spawners);

        GameManagerDungeon.Instance.SetPlayerPosition(m_playerStart);

        BossManager.Instance.SetBossSpawnLocation(m_furtherestFromPlayer);
    }

    private HashSet<Vector2Int> CreateRandomRoomShape()
    {
        HashSet<Vector2Int> tiles = new();

        foreach (BoundsInt room in m_rooms)
        {
            HashSet<Vector2Int> floors = new();
            Vector2Int roomCenter = Vector2Int.RoundToInt(room.center);
            DebugDraw.DrawBox(new BoundsInt((Vector3Int)(roomCenter), Vector3Int.one), Color.blue);
            HashSet<Vector2Int> roomFloor = ProcGenAlgorithms.MultiRandomWalk(roomCenter, m_rndWalkIterations, m_walkLength, false);

            foreach (Vector2Int position in roomFloor)
            {
                if ((room.xMin - m_offsetBetweenRooms) <= position.x && position.x <= (room.xMax - m_offsetBetweenRooms) &&
                   (room.yMin - m_offsetBetweenRooms) <= position.y && position.y <= (room.yMax - m_offsetBetweenRooms))
                {
                    floors.Add(position);
                }
            }

            m_roomFloors.Add(floors);
            tiles.UnionWith(floors);
        }

        return tiles;
    }

    private HashSet<Vector2Int> ConnectRooms()
    {
        List<Vector2Int> centers = new List<Vector2Int>(m_roomCenters);
        HashSet<Vector2Int> corridors = new();

        Vector2Int startCenter = centers[Random.Range(0, centers.Count)];
        centers.Remove(startCenter);
        float furtherestDistFromPlayer = -1.0f;

        m_playerStart = startCenter;

        while (centers.Count > 0)
        {
            Vector2Int closest = Vector2Int.zero;
            float sqrDist = float.MaxValue;
            foreach (Vector2Int center in centers)
            {
                float roomDistance = (startCenter - center).sqrMagnitude;
                float distFromPlayer = (m_playerStart - center).sqrMagnitude;
                if (roomDistance < sqrDist)
                {
                    closest = center;
                    sqrDist = roomDistance;
                }

                if (furtherestDistFromPlayer < distFromPlayer)
                {
                    m_furtherestFromPlayer = center;
                    furtherestDistFromPlayer = distFromPlayer;
                }

            }

            centers.Remove(closest);
            HashSet<Vector2Int> corridor = CreateCorridor(startCenter, closest);
            startCenter = closest;
            corridors.UnionWith(corridor);
        }

        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int startCenter, Vector2Int endCenter)
    {
        HashSet<Vector2Int> corridor = new();

        Vector2Int position = startCenter;
        corridor.Add(position);
        while (position.x != endCenter.x)
        {
            if (position.x < endCenter.x)
            {
                position += Vector2Int.right;
            }
            else
            {
                position += Vector2Int.left;
            }

            corridor.Add(position);
            corridor.Add(position - Vector2Int.down);
        }

        for(int x = -1; x < 2; x++)
        {
            for(int y = -1; y < 2; y++)
            {
                corridor.Add(position + new Vector2Int(x, y));
            }
        }

        while (position.y != endCenter.y)
        {
            if (position.y < endCenter.y)
            {
                position += Vector2Int.up;
            }
            else
            {
                position += Vector2Int.down;
            }

            corridor.Add(position);
            corridor.Add(position + Vector2Int.right);
        }

        return corridor;
    }
}
