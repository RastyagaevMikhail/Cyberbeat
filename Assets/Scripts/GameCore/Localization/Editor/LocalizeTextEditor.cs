#if UNITY_EDITOR
using UnityEditor;

using UnityEngine;

namespace GameCore.Editor
{
    using Editor = UnityEditor.Editor;
    using GameCore;
    [CustomEditor (typeof (LocalizeText))]
    public class LocalizeTextEditor : Editor
    {
        private readonly GUILayoutOption[] defaultGUI = new GUILayoutOption[0];
        public Localizator localizator { get { return Localizator.instance; } }
        public override void OnInspectorGUI ()
        {
            DrawDefaultInspector ();

            EditorGUILayout.Separator ();

            LocalizeText locText = (LocalizeText) target;

            string[] values = localizator.GetValuesForKey (locText.Id);

            for (int i = 0; i < values.Length; i++)
            {
                GUILayout.BeginHorizontal (defaultGUI);
                EditorGUILayout.LabelField (localizator.Languages[i].ToString (), defaultGUI);

                var newText = EditorGUILayout.TextField (values[i], defaultGUI);
                if (newText.Equals (values[i]) == false)
                {
                    localizator.SaveLocalization (locText.Id, localizator.Languages[i], newText);
                    EditorUtility.SetDirty (target);
                }

                GUILayout.EndHorizontal ();
            }

        }
    }
}
#endif
