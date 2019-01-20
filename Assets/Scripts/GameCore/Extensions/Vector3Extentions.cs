using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
    public static class Vector3Extentions
    {
        public static Vector3 with_x (this Vector3 value, float x)
        {
            value.x = x;
            return value;
        }
        public static Vector3 with_y (this Vector3 value, float y)
        {
            value.y = y;
            return value;
        }
        public static Vector3 with_z (this Vector3 value, float z)
        {
            value.z = z;
            return value;
        }
        public static Vector3 add_x (this Vector3 value, float x)
        {
            value.x += x;
            return value;
        }
        public static Vector3 add_y (this Vector3 value, float y)
        {
            value.y += y;
            return value;
        }
        public static Vector3 add_z (this Vector3 value, float z)
        {
            value.z += z;
            return value;
        }
        public static Vector2 To2 (this Vector3 value)
        {
            return value;
        }
        public static float Angle (this Vector3 orign, Vector3 other)
        {
            return Vector3.Angle (orign, other);
        }
        public static float Angle (this Vector2 orign, Vector2 other)
        {
            return Vector2.Angle (orign, other);
        }
        public static Vector3 Project (this Vector3 orign, Vector3 normal)
        {
            return Vector3.Project (orign, normal);
        }
        public static Vector3 ProjectOnPlane (this Vector3 orign, Vector3 normal)
        {
            return Vector3.ProjectOnPlane (orign, normal);
        }
        public static Vector2 Abs (this Vector2 vector)
        {
            return new Vector2 (vector.x.Abs (), vector.y.Abs ());
        }
        public static Vector3 Abs (this Vector3 vector)
        {
            return new Vector3 (vector.x.Abs (), vector.y.Abs (), vector.z.Abs ());
        }
        public static Vector4 Abs (this Vector4 vector)
        {
            return new Vector4 (vector.x.Abs (), vector.y.Abs (), vector.z.Abs (), vector.w.Abs ());
        }
    }
}
