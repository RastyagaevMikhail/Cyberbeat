using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

using System;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace GameCore.Editor
{
    using GameCore;

    using Sirenix.Utilities;

    public abstract class ScriptableObjectDrawer<TObject> : OdinValueDrawer<TObject> where TObject : ScriptableObject
    {
        protected virtual string LocalFolderPath { get { return "Assets/Data/"; } }
        protected virtual System.Action<IPropertyValueEntry<TObject>> OnCreated { get { return entry => { ChechAction<TObject> (entry); }; } }
        Dictionary<Type, Action<IPropertyValueEntry<TObject>>> ActionSelector = new Dictionary<Type, Action<IPropertyValueEntry<TObject>>> ()
        { { typeof (ISavableVariable), SeteCategotyTagFromSavableVariable }
        };

        static void SeteCategotyTagFromSavableVariable (IPropertyValueEntry<TObject> entry)
        {
            (entry.SmartValue as ISavableVariable).CategoryTag = entry.ParentType.Name;
        }
        void ChechAction<TValue> (IPropertyValueEntry<TObject> entry)
        {
            Type type = typeof (TValue);
            if (ActionSelector.ContainsKey (type))
                ActionSelector[type] (entry);
        }
        protected override void DrawPropertyLayout (IPropertyValueEntry<TObject> entry, GUIContent label)
        {
            var value = entry.SmartValue;
            GUILayout.BeginHorizontal ();
            {
                entry.SmartValue = SirenixEditorFields.UnityObjectField (label, value, entry.TypeOfValue, false) as TObject;
                if (entry.SmartValue != null)
                {
                    entry.ApplyChanges ();
                    GUILayout.EndHorizontal ();
                    return;
                }
                bool create = SirenixEditorGUI.IconButton (EditorIcons.Plus, GUI.skin.button);
                if (create)
                {
                    var newSO = ScriptableObject.CreateInstance<TObject> ();

                    string title = "Save {0}".AsFormat (entry.TypeOfValue.Name);
                    string localPathWithParent = LocalFolderPath + "{0}/".AsFormat (entry.ParentType.Name);
                    string assetPath = (localPathWithParent + "{0}.asset").AsFormat (label.text);
                    Tools.ValidatePath (assetPath);
                    // Debug.LogFormat ("assetPath = {0}", assetPath);
                    string path = EditorUtility.SaveFilePanelInProject (title, label.text, "asset", title, localPathWithParent);
                    // Debug.LogFormat ("path = {0}", path);
                    if (path.IsNullOrEmpty ()) return;
                    newSO.CreateAsset (assetPath);
                    entry.SmartValue = newSO;
                    entry.ApplyChanges ();
                    OnCreated (entry);
                }

            }
            GUILayout.EndHorizontal ();

        }

    }
}
