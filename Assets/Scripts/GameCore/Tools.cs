using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Random = UnityEngine.Random;
namespace GameCore
{
    public static class Tools
    {
        public static T RandomIn<T> (IEnumerable<T> collection)
        {
            T[] array = collection.ToArray ();
            return array[Random.Range (0, array.Length)];
        }
        public static bool RandomBool { get { return Random.value >= 0.5f; } }
        public static int RandomOne { get { return RandomBool ? 1 : -1; } }

        public static int RandomZero { get { return RandomBool ? 1 : 0; } }
        public static IEnumerable<object> ShuffleObjects (IEnumerable<object> collection)
        {
            List<object> result = new List<object> ();
            List<object> originCollection = new List<object> (collection);
            int count = collection.Count ();
            for (int i = 0; i < count; i++)
            {
                object tmp = originCollection[UnityEngine.Random.Range (0, originCollection.Count)];
                result.Add (tmp);
                originCollection.Remove (tmp);
            }
            return result;
        }

        public static Action EmptyAction = () => { };

        public static IEnumerable<T> ShuffleObjects<T> (IEnumerable<T> collection)
        {
            List<T> result = new List<T> ();
            List<T> originCollection = new List<T> (collection);
            int count = collection.Count ();
            for (int i = 0; i < count; i++)
            {
                T tmp = originCollection[UnityEngine.Random.Range (0, originCollection.Count)];
                result.Add (tmp);
                originCollection.Remove (tmp);
            }
            return result;
        }
        static void Swap<T> (ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        public static void DelayAction (MonoBehaviour monoBehaviour, float timeDelay, Action action)
        {
            monoBehaviour.StartCoroutine (DelayCoroutine (timeDelay, action));
        }

        private static IEnumerator DelayCoroutine (float timeDelay, Action action)
        {

            yield return new WaitForSeconds (timeDelay);
            if (action != null)
                action ();
        }

        public static Texture GetTextureFromSprite (Sprite sprite)
        {
            Rect textureRect = sprite.textureRect;
            int blockWidth = Mathf.CeilToInt (textureRect.width);
            int blockHeight = Mathf.CeilToInt (textureRect.height);
            var croppedTexture = new Texture2D (blockWidth, blockHeight);
            var pixels = sprite.texture.GetPixels (
                (int) textureRect.x,
                (int) textureRect.y,
                blockWidth,
                blockHeight);
            croppedTexture.SetPixels (pixels);
            croppedTexture.Apply ();
            return croppedTexture;
        }

        public static string LogTextInColor (string text, Color color)
        {
            return string.Format ("<b><color=\"#{0}\">{1}</color></b>", ColorUtility.ToHtmlStringRGB (color), text);
        }

        public static void Destroy (UnityEngine.Object obj)
        {
            if (Application.isPlaying)
            {
                UnityEngine.Object.Destroy (obj);
            }
#if UNITY_EDITOR
            else
            {
                UnityEditor.Editor.DestroyImmediate (obj);
            }
#endif
        }

        public static void LogCollections (params object[][] collections)
        {
            string result = "";
            List<object> FirstCollection = collections.FirstOrDefault ().ToList ();
            if (!collections.ToList ().TrueForAll (c => c.Count () == FirstCollection.Count))
            {
                Debug.Log ("Размеры коллекций не совпадает");
                return;
            }
            string format = "";
            int count = collections.Length;
            for (int i = 0; i < count; i++)
            {
                string v = string.Format ("{{0}}___!!!___", i);
                // Debug.LogFormat ("v = {0}", v);
                format += v;
            }

            for (int i = 0; i < FirstCollection.Count (); i++)
            {
                object[] args = collections.Select (c => c.ElementAt (i)).ToArray ();
                // args.ToList ().ForEach (a => Debug.LogFormat ("a = {0}", a));
                foreach (var a in args)
                {
                    result += string.Format (format, a as object[]);
                }
                result += '\n';
            }
            Debug.Log (result);
        }
        public static string AddSpace (string mystring)
        {
            // return mystring.PrependIf (c => (c as string).IsUpper (), ' ').ToString();
            string result = "";
            for (int i = 0; i < mystring.Length; i++)
                result += (mystring[i].IsUpper () ? " " : "") + mystring[i];
            return result;
        }
        public static string LogCollection<T> (IEnumerable<T> collection)
        {
            return LogCollection (collection, i => i);
        }
        public static string LogCollection<T, TResult> (IEnumerable<T> collection, Func<T, TResult> selector, string Format = "{0}", string separator = "\n", params object[] ps)
        {
            string result = "";
            result = collection != null ? collection.Count ().ToString () : "Null";
            result += " items {0}\n------\n".AsFormat (typeof (T).Name);

            foreach (var item in collection.Select (selector))
            {
                var parameters = new object[] { item }.Concat (ps).ToArray ();
                result += string.Format (Format + separator, parameters);
            }
            return result;
        }
        public static void LogColor (Color color, bool useAlpha = false)
        {
            Debug.LogFormat ("#{0}", color.ToString (useAlpha));
        }
        public static void SetBool (string HashKey, bool value)
        {
            PlayerPrefs.SetInt (HashKey, value ? 1 : 0);
        }
        public static bool GetBool (string HashKey, bool deafultValue = false)
        {
            return PlayerPrefs.GetInt (HashKey, deafultValue ? 1 : 0) == 1;
        }

#if UNITY_EDITOR
        public static T[] GetAtPath<T> (string path) where T : UnityEngine.Object
        {
            // Debug.LogFormat ("path = {0}", path);
            string filterByType = string.Format ("t:{0}", typeof (T).Name);
            // Debug.LogFormat ("filterByType = {0}", filterByType);
            var guids = AssetDatabase.FindAssets (filterByType, new string[] { path });

            T[] results = guids.Select (guid => (T) AssetDatabase.LoadAssetAtPath (AssetDatabase.GUIDToAssetPath (guid), typeof (T))).ToArray ();
            // Debug.LogFormat ("results.Count() = {0}", results.Count ());
            return results;
        }
        public static UnityEngine.Object[] GetAtPath (string path, Type typeAsset)
        {
            // Debug.LogFormat ("path = {0}", path);
            string filterByType = string.Format ("t:{0}", typeAsset.Name);
            // Debug.LogFormat ("filterByType = {0}", filterByType);
            var guids = AssetDatabase.FindAssets (filterByType, new string[] { path });

            var results = guids.Select (guid => AssetDatabase.LoadAssetAtPath (AssetDatabase.GUIDToAssetPath (guid), typeAsset)).ToArray ();
            // Debug.LogFormat ("results.Count() = {0}", results.Count ());
            return results;
        }
        public static UnityEngine.Object[] GetAtPath (string path, string typeAsset)
        {
            // Debug.LogFormat ("path = {0}", path);
            string filterByType = string.Format ("t:{0}", typeAsset);
            // Debug.LogFormat ("filterByType = {0}", filterByType);
            var guids = AssetDatabase.FindAssets (filterByType, new string[] { path });

            var results = guids.Select (guid => AssetDatabase.LoadAssetAtPath (AssetDatabase.GUIDToAssetPath (guid), typeof (UnityEngine.Object))).ToArray ();
            // Debug.LogFormat ("results.Count() = {0}", results.Count ());
            return results;
        }
        public static T GetAssetAtPath<T> (string path) where T : UnityEngine.Object
        {
            return (T) AssetDatabase.LoadAssetAtPath (path, typeof (T));
        }
        public static void CreateAsset<T> (T asset, string path) where T : UnityEngine.Object
        {
            ValidatePath (path);
            UnityEngine.Object oldAssetInctance = AssetDatabase.LoadAssetAtPath (path, typeof (T));
            if (oldAssetInctance)
            {
                AssetDatabase.DeleteAsset (path);
                Destroy (oldAssetInctance);
            }

            EditorUtility.SetDirty (asset);
            AssetDatabase.CreateAsset (asset, path);
            AssetDatabase.SaveAssets ();
        }

        public static void ValidatePath (string path)
        {
            var m = Regex.Match (path, "([a-zA-Z0-9 ])+(.asset)");
            // Debug.LogFormat ("regex \"{0}\" in Path {1}", m.Value, path);
            path = path.Replace ("/" + m.Value, "");
            string[] splitedPath = path.Split ('/');
            string validatedPath = splitedPath[0];

            List<string> list = splitedPath.ToList ();
            list.RemoveAt (0);
            splitedPath = list.ToArray ();

            foreach (var subPath in splitedPath)
            {
                if (!AssetDatabase.IsValidFolder (validatedPath + "/" + subPath))
                {
                    AssetDatabase.CreateFolder (validatedPath, subPath);
                }
                validatedPath += "/" + subPath;
            }
        }

        public class EditorAssembliesHelper
        {
            static Assembly _assembly = null;
            static Assembly assembly { get { if (_assembly == null) _assembly = System.Reflection.Assembly.Load ("Assembly-CSharp-Editor"); return _assembly; } }
            public static Type GetType (string TypeName)
            {
                return assembly.GetType (TypeName);
            }
            public static MethodInfo GetMethodInCalss (string TypeName, string MethodName)
            {
                return GetType (TypeName).GetMethod (MethodName);
            }
            public static FieldInfo GetFieldInCalss (string TypeName, string FieldName)
            {
                return GetType (TypeName).GetField (FieldName);
            }
            public static PropertyInfo GetPropertyInCalss (string TypeName, string PropertyName)
            {
                return GetType (TypeName).GetProperty (PropertyName);
            }

        }
        //TODO Separate From GameCore namesapace
        private static GUIStyle backdropHtmlLabel = null;
        public static GUIStyle BackdropHtmlLabel
        {
            get
            {
                if (backdropHtmlLabel == null)
                {
                    var propInfo = EditorAssembliesHelper.GetPropertyInCalss ("FluffyUnderware.DevToolsEditor.DTStyles", "BackdropHtmlLabel");
                    backdropHtmlLabel = (GUIStyle) propInfo.GetValue (null, null);
                }
                return backdropHtmlLabel;
            }
        }

#endif
    }

}
