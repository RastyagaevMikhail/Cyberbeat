namespace GameCore.Editor
{
    using Sirenix.Utilities.Editor;

    using UnityEditor;

    using UnityEngine;
    [CustomEditor (typeof (GameEvent))]
    public class EventEditor : Editor
    {
        GameEvent e = null;
        private void OnEnable ()
        {
            e = target as GameEvent;

        }
        public override void OnInspectorGUI ()
        {
            base.OnInspectorGUI ();

            GUI.enabled = Application.isPlaying;

            if (GUILayout.Button ("Raise"))
                e.Raise ();
        }
    }

    [CustomPropertyDrawer (typeof (GameEvent))]
    public class GameEventDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.objectReferenceValue != null)
            {
                label = EditorGUI.BeginProperty (position, label, property);
                EditorGUI.PropertyField (position, property, label);
                EditorGUI.EndProperty ();

                return;
            }
            var mControlRect = position;
            label = EditorGUI.BeginProperty (position, label, property);
            mControlRect.width -= 20;
            EditorGUI.PropertyField (mControlRect, property, label);
            mControlRect.x = mControlRect.xMax + 2;
            mControlRect.width = 20;
            if (GUI.Button (mControlRect, new GUIContent (EditorIcons.Plus.Raw, "Add")))
            {
                // Call AddResource to create and name the resource
                var newEvent = ScriptableObject.CreateInstance<GameEvent> ();

                const string PathForamt = "Assets/Data/Events/{0}.asset";
                string path = string.Format (PathForamt, property.name);
                AssetDatabase.CreateAsset (newEvent, path);
                AssetDatabase.SaveAssets ();
                property.objectReferenceValue = newEvent;
                Selection.activeObject = newEvent;
            }
            EditorGUI.EndProperty ();
        }
    }
}
