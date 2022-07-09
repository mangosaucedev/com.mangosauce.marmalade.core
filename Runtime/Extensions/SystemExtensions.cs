using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade
{
    public static class SystemExtension
    {
        public static int RoundUpToInt(this float number)
        {
            return Mathf.FloorToInt(number + 0.5f);
        }

        public static int RoundDownToInt(this float number)
        {
            return Mathf.CeilToInt(number - 0.5f);
        }

        public static float RoundToDecimal(this float f, int decimals)
        {
            return (float)Math.Round((decimal)f, decimals);
        }

        public static float ToAngle(this Vector2 vector) =>
            Mathf.Atan2(vector.y, vector.x);
        

        public static float ToAngle(this Vector2Int vector) =>
            Mathf.Atan2(vector.y, vector.x);


        public static Vector2Int ToVectorIntNormalized(this Vector2 vector)
        {
            vector = vector.normalized;
            int x = Mathf.Clamp(Mathf.RoundToInt(vector.x), -1, 1);
            int y = Mathf.Clamp(Mathf.RoundToInt(vector.y), -1, 1);
            return new Vector2Int(x, y);
        }
    }
}
