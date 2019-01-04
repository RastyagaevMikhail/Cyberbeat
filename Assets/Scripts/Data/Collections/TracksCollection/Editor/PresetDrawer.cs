/* using UnityEditor;

using UnityEngine;

namespace CyberBeat.Drawers
{
    [CustomPropertyDrawer (typeof (Preset))]
    public class PresetDrawer : PropertyDrawer
    {
        SerializedProperty Id = null;
        SerializedProperty Objects = null;
        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2;
        }
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            InitProperties (property);
            var rectId = new Rect (position) { height = position.height / 2 };
            EditorGUI.PropertyField (rectId, Id);
            var rectObjects = new Rect (position) { height = position.height / 2, y =  EditorGUIUtility.singleLineHeight/2f };
            EditorGUI.PropertyField (rectObjects, Objects);
        }

        private void InitProperties (SerializedProperty property)
        {
            if (Id == null)
                Id = property.FindPropertyRelative ("Id");
            if (Objects == null)
                Objects = property.FindPropertyRelative ("Objects");
        }
    }
}
 */