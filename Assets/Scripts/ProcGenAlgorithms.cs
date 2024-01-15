using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ProcGenAlgorithms
{
    private const float PROB_VERTICALLY_SPLIT = 0.4f;
    private const float PROB_HORIZONTALLY_SPLIT = 1.0f - PROB_VERTICALLY_SPLIT;

    public static HashSet<Vector2Int> MultiRandomWalk(Vector2Int startPosition, int walkLength, int iterations, bool startRandom)
    {
        Vector2Int position = startPosition;
        HashSet<Vector2Int> walkPositions = new();

        for(int i = 0; i < iterations; i++)
        {
            HashSet<Vector2Int> randomWalk = RandomWalk(position, walkLength);
            walkPositions.UnionWith(randomWalk);

            if(startRandom)
            {
                position = walkPositions.ElementAt(Random.Range(0, walkPositions.Count));
            }
        }

        return walkPositions;
    }

    public static HashSet<Vector2Int> RandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new();

        path.Add(startPosition);
        Vector2Int previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            Vector2Int position = previousPosition + Direction2d.RandomCardinalDirection();
            path.Add(position);
            previousPosition = position;
        }

        return path;
    }

    public static List<BoundsInt> PartitionArea(BoundsInt areaToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> rooms = new();
        List<BoundsInt> roomsList = new();

        rooms.Enqueue(areaToSplit);
        while (rooms.Count > 0)
        {
            BoundsInt room = rooms.Dequeue();

            if (room.size.x < minWidth || room.size.y < minHeight)
            {
                roomsList.Add(room);
                continue;
            }

            if (Random.value < PROB_HORIZONTALLY_SPLIT)
            {
                if (room.size.y >= minHeight * 2.0f)
                {
                    SplitHorizontally(minHeight, rooms, room);
                }
                else if(room.size.x >= minWidth * 2.0f)
                {
                    SplitVertically(minWidth, rooms, room);
                }
                else
                {
                    roomsList.Add(room);
                }
            }
            else
            {
                if (room.size.x >= minWidth * 2.0f)
                {
                    SplitVertically(minWidth, rooms, room);
                }
                else if (room.size.y >= minHeight * 2.0f)
                {
                    SplitHorizontally(minHeight, rooms, room);
                }
                else
                {
                    roomsList.Add(room);
                }
            }
        }

        for (int i = 0; i < roomsList.Count; i++)
        {
            DebugDraw.DrawBox(roomsList[i], Color.red);
        }

        return roomsList;
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> rooms, BoundsInt room)
    {
        int yCoord = Random.Range(minHeight, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, yCoord, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + yCoord, room.min.z), new Vector3Int(room.size.x, room.size.y - yCoord, room.size.z));

        rooms.Enqueue(room1);
        rooms.Enqueue(room2);
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> rooms, BoundsInt room)
    {
        int xCoord = Random.Range(minWidth, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xCoord, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xCoord, room.min.y, room.min.z), new Vector3Int(room.size.x - xCoord, room.size.y, room.size.z));

        rooms.Enqueue(room1);
        rooms.Enqueue(room2);
    }
}
