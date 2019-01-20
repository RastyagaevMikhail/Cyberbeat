using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace GameCore
{
    public static class SimpleExtentions
    {
        public static bool IsNullOrEmpty<T> (this IList<T> list)
        {
            return list == null || list.Count == 0;
        }
        public static T GetRandom<T> (this IEnumerable<T> collection)
        {
            T[] array = collection.ToArray ();
            return array[Random.Range (0, array.Length)];
        }
        public static bool InRange (this float value, float Min, float Max)
        {
            return Min <= value && value <= Max;
        }
        public static bool InRange (this int value, int Min, int Max)
        {
            return Min <= value && value <= Max;
        }
        public static bool InRangeIndex<T> (this IEnumerable<T> collection, int index)
        {
            return index.InRange (0, collection.Count () - 1);
        }
        public static float ToRad (this float value)
        {
            return Mathf.Deg2Rad * value;
        }
        public static float ToDeg (this float value)
        {
            return Mathf.Rad2Deg * value;
        }
        public static float Sqr (this float value)
        {
            return Mathf.Pow (value, 2);
        }
        public static float Sqrt (this float value)
        {
            return Mathf.Sqrt (value);
        }
        public static float Abs (this float value)
        {
            return Mathf.Abs (value);
        }
        public static int Abs (this int value)
        {
            return Mathf.Abs (value);
        }
        public static int CeilToInt (this float value)
        {
            return Mathf.CeilToInt (value);
        }
        public static float Round (this float value)
        {
            return Mathf.Round (value);
        }
        public static int RoundToInt (this float value)
        {
            return Mathf.RoundToInt (value);
        }
        public static float Sign (this float value)
        {
            return Mathf.Sign (value);
        }
        public static int Sign (this int value)
        {
            return (int) Mathf.Sign (value);
        }
        public static float Pow (this float value, float p)
        {
            return Mathf.Pow (value, p);
        }
        public static bool Approximately (this float value, float other)
        {
            return Mathf.Approximately (value, other);
        }
        public static void Clamp01 (this float value)
        {
            value = Mathf.Clamp01 (value);
        }

        public static void Clamp (this float value, float min, float max)
        {
            value = value.GetAsClamped (min, max);
        }
        public static void Clamp (this int value, int min, int max)
        {
            value = value.GetAsClamped (min, max);
        }
        public static float GetAsClamped (this float value, float min, float max)
        {
            return Mathf.Clamp (value, min, max);
        }
        public static int GetAsClamped (this int value, int min, int max)
        {
            return Mathf.Clamp (value, min, max);
        }
        public static float LerpTo (this float value, float to, float t)
        {
            return Mathf.Lerp (value, to, t);
        }
        public static float LerpAsTime (this float t, float from, float to)
        {
            return Mathf.Lerp (from, to, t);
        }
    }
}
