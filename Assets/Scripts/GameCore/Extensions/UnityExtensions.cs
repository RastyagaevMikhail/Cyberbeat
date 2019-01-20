using System.Collections;

using UnityEditor;

using UnityEngine;
using Object = UnityEngine.Object;
namespace GameCore
{

    public static class UnityExtensions
    {
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
            return fromColor ? colorName.ToColor (color) : colorName;
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

        public static void DelayAction (this MonoBehaviour mono, float timeDelay, System.Action action)
        {
            mono.StartCoroutine (cr_DelayAction (timeDelay, action));
        }
        private static IEnumerator cr_DelayAction (float timeDelay, System.Action action)
        {
            yield return new WaitForSeconds (timeDelay);
            if (action != null)
                action ();
        }

        public static void SetRect (this RectTransform rt, float left = 0f, float top = 0f, float right = 0f, float bottom = 0f)
        {
            rt.offsetMin = new Vector2 (left, bottom);
            rt.offsetMax = new Vector2 (-right, -top);
        }
        public static void SetFullSizeOfParent (this RectTransform rt, Vector2 anchoredPosition = new Vector2 ())
        {
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.sizeDelta = Vector2.zero;
            rt.anchoredPosition = anchoredPosition;
        }
        public static void SetRect (this RectTransform rt, float all)
        {
            rt.SetRect (all, all, all, all);
        }

    }
}
