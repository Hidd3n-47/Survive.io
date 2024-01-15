using UnityEngine;

public static class DebugDraw
{
    public static void DrawBox(BoundsInt bounds, Color color)
    {
        Debug.DrawLine(new Vector3(bounds.xMin, bounds.yMin, 0.0f), 
            new Vector3(bounds.xMin, bounds.yMax, 0.0f), color, 100.0f);

        Debug.DrawLine(new Vector3(bounds.xMin, bounds.yMin, 0.0f), 
            new Vector3(bounds.xMax, bounds.yMin, 0.0f), color, 100.0f);

        Debug.DrawLine(new Vector3(bounds.xMin, bounds.yMax, 0.0f), 
            new Vector3(bounds.xMax, bounds.yMax, 0.0f), color, 100.0f);

        Debug.DrawLine(new Vector3(bounds.xMax, bounds.yMin, 0.0f), 
            new Vector3(bounds.xMax, bounds.yMax, 0.0f), color, 100.0f);
    }
}