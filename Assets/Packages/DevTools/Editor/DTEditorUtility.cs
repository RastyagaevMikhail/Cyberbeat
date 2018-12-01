// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;
using FluffyUnderware.DevTools.Extensions;
using System.Collections;

namespace FluffyUnderware.DevToolsEditor
{
    public static class DTEditorUtility
    {
        public static Camera ActiveCamera
        {
            get
            {
                return (SceneView.currentDrawingSceneView) ? SceneView.currentDrawingSceneView.camera : Camera.current;
            }
        }

        public static bool DragDropTypeMatch(System.Type type)
        {
            foreach (Object go in DragAndDrop.objectReferences)
            {
                if (go is GameObject)
                {
                    if (((GameObject)go).GetComponent(type) == null)
                        return false;
                }

                else if (go.GetType() != type)
                    return false;
            }

            return true;
        }

        public static bool DragDropTypeMatch<T>() where T : Object
        {
            return DragDropTypeMatch(typeof(T));
        }

        public static Object[] DragDropGetObjectsOfType(System.Type type)
        {
            if (type.IsArrayOrList())
                return new Object[0];
            List<Object> res = new List<Object>();
            foreach (Object o in DragAndDrop.objectReferences)
            {
                if (o.GetType() == type)
                    res.Add(o);
                else if (o is GameObject)
                {
                    var f = ((GameObject)o).GetComponent(type);
                    if (f != null)
                        res.Add(f);
                }
            }
            return res.ToArray();
        }

        public static T CreateAsset<T>() where T : ScriptableObject
        {
            var asset = ScriptableObject.CreateInstance<T>();
            ProjectWindowUtil.CreateAsset(asset, "New " + typeof(T).Name + ".asset");
            AssetDatabase.SaveAssets();
            return asset;
        }

        public static void ToggleSceneViewGizmos(bool gizmosOn)
        {
            ToggleSceneViewGizmos(gizmosOn, new string[0]);
        }

        public static void ToggleSceneViewGizmos(bool gizmosOn, params string[] scriptClasses)
        {
            int val = gizmosOn ? 1 : 0;
            List<string> classes = new List<string>(scriptClasses);
            Assembly asm = Assembly.GetAssembly(typeof(Editor));
            System.Type type = asm.GetType("UnityEditor.AnnotationUtility");
            if (type != null)
            {
                MethodInfo getAnnotations = type.GetMethod("GetAnnotations", BindingFlags.Static | BindingFlags.NonPublic);
                MethodInfo setGizmoEnabled = type.GetMethod("SetGizmoEnabled", BindingFlags.Static | BindingFlags.NonPublic);
                MethodInfo setIconEnabled = type.GetMethod("SetIconEnabled", BindingFlags.Static | BindingFlags.NonPublic);
                var annotations = getAnnotations.Invoke(null, null);
                foreach (object annotation in (IEnumerable)annotations)
                {
                    System.Type annotationType = annotation.GetType();
                    FieldInfo classIdField = annotationType.GetField("classID", BindingFlags.Public | BindingFlags.Instance);
                    FieldInfo scriptClassField = annotationType.GetField("scriptClass", BindingFlags.Public | BindingFlags.Instance);
                    if (classIdField != null && scriptClassField != null)
                    {
                        int classId = (int)classIdField.GetValue(annotation);
                        string scriptClass = (string)scriptClassField.GetValue(annotation);
                        if (classes.Count == 0 || classes.Contains(scriptClass))
                        {
                            setGizmoEnabled.Invoke(null, new object[] { classId, scriptClass, val });
                            setIconEnabled.Invoke(null, new object[] { classId, scriptClass, val });
                        }
                    }
                }
            }
        }

        public static List<T> LoadPrefabsContaining<T>(string path) where T : UnityEngine.Component
        {
            List<T> res = new List<T>();

            var gos = AssetDatabase.FindAssets("t:gameobject", new string[] { path });
            foreach (var id in gos)
            {
                GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(id));
                T cmp = go.GetComponent<T>();
                if (cmp != null)
                    res.Add(cmp);
            }
            return res;
        }

    }
}
