using DG.Tweening;

using Sirenix.OdinInspector;

using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;
using Tools = GameCore.Tools;
namespace GameCore
{

    public static class UnityExtensions
    {
        [Button]
        public static void Save (this ScriptableObject so)
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty (so);
            AssetDatabase.SaveAssets ();
            AssetDatabase.Refresh ();
#endif
        }

        public static void CreateAsset (this ScriptableObject so, string path)
        {
#if UNITY_EDITOR
            Tools.CreateAsset (so, path);
#endif
        }
        public static void ReNameWithID (this Object obj)
        {
            obj.name = "{0}{1}".AsFormat (obj.name, obj.GetInstanceID ());
        }
        public static string ToString (this Color color, bool useAlpha = false, bool fromColor = false)
        {
            string colorName = useAlpha ? ColorUtility.ToHtmlStringRGBA (color) : ColorUtility.ToHtmlStringRGB (color);
            return fromColor ? Tools.LogTextInColor (colorName, color) : colorName;
        }

        public static T GetOrAddComponent<T> (this GameObject go) where T : Component
        {
            T component = go.GetComponent<T> ();
            if (component == null) component = go.AddComponent<T> ();
            return component;
        }
        public static T GetOrAddComponent<T> (this Transform transform) where T : Component
        {
            T component = transform.GetComponent<T> ();
            if (component == null) component = transform.gameObject.AddComponent<T> ();
            return component;
        }
    }

    public static class TransformExtensions
    {
        public static void CenterOnChildred (this Transform aParent)
        {
            var childs = aParent.Cast<Transform> ().ToList ();
            var pos = Vector3.zero;
            foreach (var C in childs)
            {
                pos += C.position;
                C.parent = null;
            }
            pos /= childs.Count;
            aParent.position = pos;
            foreach (var C in childs)
                C.parent = aParent;
        }
        public static void DestroyAllChilds (this Transform transform)
        {
            transform.Cast<Transform> ().ToList ().ForEach (t => Tools.Destroy (t.gameObject));
        }
    }

    public static class SimpleExtentions
    {
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
        public static string WithColor (this string str, Color color)
        {
            return Tools.LogTextInColor (str, color);
        }
        public static string FromString (this Color color, string str)
        {
            return Tools.LogTextInColor (str, color);
        }

        public static string AsFormat (this string format, params object[] ps)
        {
            return string.Format (format, ps);
        }
        public static bool IsUpper (this char c)
        {
            return System.Char.IsUpper (c);
        }
        public static bool IsLower (this char c)
        {
            return System.Char.IsLower (c);
        }
        public static bool IsUpper (this string c)
        {
            return c.SingleOrDefault ().IsUpper ();
        }
        public static bool IsLower (this string c)
        {
            return c.SingleOrDefault ().IsLower ();
        }
        public static string ToCapitalize (this string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper (str[0]) + str.Substring (1);

            return str.ToUpper ();
        }
        public static bool IsNullOrEmpty (this string str)
        {
            return string.IsNullOrEmpty (str);
        }
        public static void Do (this float value, float endValue, float duration, TweenCallback<float> onVirtualUpdate = null)
        {
            TweenCallback<float> DefaultCallback = v => value = v;
            DOVirtual.Float (value, endValue, duration, onVirtualUpdate == null? DefaultCallback : onVirtualUpdate);
        }
    }
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
