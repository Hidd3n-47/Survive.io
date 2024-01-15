using UnityEngine;

public static class Direction2d
{
    public static Vector2Int[] cardinalDirections =
         { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
    public static Vector2Int[] cardinalAndDiagonalDirections =
         { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left ,
            new Vector2Int(1,1), new Vector2Int(-1, 1), new Vector2Int(-1, -1), new Vector2Int(1, -1) };

    public static Vector2Int RandomCardinalDirection()
    {
        return cardinalDirections[Random.Range(0, cardinalDirections.Length)];
    }

}
