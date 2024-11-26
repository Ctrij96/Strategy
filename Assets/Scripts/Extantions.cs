using System.Collections.Generic;
using UnityEngine;

public static class Extantions
{
    public static Vector3 Vector2ToHorizontal(this Vector2 toV3)
    {
        return new Vector3(toV3.x, 0f, toV3.y);
    }
    
}