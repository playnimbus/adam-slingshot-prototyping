using System;
using UnityEngine;

public static class HelperMethods
{
    public static Vector2 Vector2FromDegrees(float degrees)
    {
        return Vector2FromRadians(Mathf.Deg2Rad * degrees);
    }

    public static Vector2 Vector2FromRadians(float radians)
    {
        float x = Mathf.Cos(radians);
        float y = Mathf.Sin(radians);
        return new Vector2(x, y);
    }

    // Perpindicular vector clockwise
    public static Vector2 PerpindicularVector(Vector2 direction)
    {
        return new Vector2(direction.y, -direction.x);
    }
}
