using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text.RegularExpressions;

using UnityEditor;

using UnityEngine;
namespace GameCore
{
    public class ScriptGenerationWizard : ScriptableWizard
    {
        [SerializeField] string nameOfNameSpace = "GameCore";
        [SerializeField] string nameOfType;
        [SerializeField] bool runtimeSet;
        [SerializeField] bool savableVariable;
        [SerializeField] bool gameEvent;
        [SerializeField] bool selector;
        [SerializeField] bool nameSelector;
        [SerializeField] KeyValuePairStringType[] KeysValues;

        const string namesapaceKey = "$NAMESPACE_NAME$";
        const string typeKey = "$TYPE_NAME$";
        const string typeKeyKeyType = "$TYPE_KEY$";

        [MenuItem ("Game/Generation/Script")]
        static void CreateWizard ()
        {
            var wizard = ScriptableWizard.DisplayWizard<ScriptGenerationWizard> ("Create Scripts", "Generate", "Get keys");
            var nameOfNameSpace = EditorPrefs.GetString ("nameOfNameSpace", "GameCore");
            wizard.UpdateNameSpace (nameOfNameSpace);
        }

        private void UpdateNameSpace (string nameOfNameSpace)
        {
            this.nameOfNameSpace = nameOfNameSpace;
        }

        void OnWizardCreate ()
        {
            EditorPrefs.SetString ("nameOfNameSpace", nameOfNameSpace);
            if (runtimeSet)
            {
                ScriptGenerator RuntimeSetScriptGenerator = new ScriptGenerator ()
                {
                    Name = "RuntimeSet",
                    nameOfType = nameOfType,
                    typeKey = typeKey,
                    namesapaceKey = namesapaceKey,
                    nameOfNameSpace = nameOfNameSpace
                };
                RuntimeSetScriptGenerator.Generate ();
                // Selection.activeObject = Tools.GetAssetAtPath<TextAsset> ("Assets/" + filePath.Replace (Application.dataPath, ""));
            }
            if (savableVariable)
            {
                ScriptGenerator RuntimeSetScriptGenerator = new ScriptGenerator ()
                {
                    Name = "Variable",
                    nameOfType = nameOfType,
                    typeKey = typeKey,
                    namesapaceKey = namesapaceKey,
                    nameOfNameSpace = nameOfNameSpace
                };
                RuntimeSetScriptGenerator.Generate ();
            }

            if (gameEvent)
            {
                ScriptGenerator GameEventScriptGenerator = new ScriptGenerator ()
                {
                    Name = "GameEvent",
                    nameOfType = nameOfType,
                    typeKey = typeKey,
                    namesapaceKey = namesapaceKey,
                    nameOfNameSpace = nameOfNameSpace
                };
                GameEventScriptGenerator.Generate (false);

                ScriptGenerator GameEventListenerScriptGenerator = new ScriptGenerator ()
                {
                    Name = "GameEventListener",
                    nameOfType = nameOfType,
                    typeKey = typeKey,
                    namesapaceKey = namesapaceKey,
                    nameOfNameSpace = nameOfNameSpace
                };
                GameEventListenerScriptGenerator.Generate (false);
            }

            if (selector)
            {
                ScriptGenerator SelectorScriptGenerator = new ScriptGenerator ("Selector");
                string NameScript = "$KEY_TYPE$$VALUE_TYPE$$SCRIPT_NAME$";
                foreach (var kvp in KeysValues)
                    NameScript = NameScript.Replace (kvp.Key, kvp.Value);
                SelectorScriptGenerator.GenerateByKeys (() => NameScript, KeysValues);
            }
        }

        void OnWizardUpdate ()
        {
            helpString = "Chose the Script type generation";
        }

        void OnWizardOtherButton ()
        {
            if (selector)
                parseKeys ();
            if (nameSelector)
                parseKeys ();
        }

        private void parseKeys ()
        {
            var textAsset = Resources.Load<TextAsset> (name);
            var matchs = Regex.Matches (textAsset.text, @"\$[A-Z]*_[A-Z]*\$");

            var strings = matchs.Cast<Match> ().Select (m => m.Value);
            KeysValues = new HashSet<string> (strings).Distinct ()
                .Select (m => new KeyValuePairStringType () { Key = m, Value = "" }).ToArray ();
        }
    }

    [Serializable]
    public class KeyValuePairStringType
    {
        public string Key;
        public string Value;
    }

    [CustomPropertyDrawer (typeof (KeyValuePairStringType))]
    public class KeyValuePairStringTypeDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var key = property.FindPropertyRelative ("Key");
            var value = property.FindPropertyRelative ("Value");
            var rect = EditorGUI.PrefixLabel (position, new GUIContent (key.stringValue));
            EditorGUI.PropertyField (rect, value, GUIContent.none);

        }
    }

    public class ScriptGenerator
    {
        public string Name;
        public string nameOfType;
        public string typeKey;
        public string namesapaceKey = "$NAMESPACE_NAME$";
        public string nameOfNameSpace;

        public ScriptGenerator () { }

        public ScriptGenerator (string Name)
        {
            this.Name = Name;
        }

        public void Generate (bool ForwardTypeName = true)
        {
            var textAsset = Resources.Load<TextAsset> (Name);

            string tempateText = textAsset.text;

            string ScriptText = tempateText.Replace (namesapaceKey, nameOfNameSpace);
            ScriptText = ScriptText.Replace (typeKey, nameOfType);

            string directoryPath = Application.dataPath + "/Scripts/" + nameOfNameSpace + "/" + Name + "/";
            Directory.CreateDirectory (directoryPath);
            string name = ForwardTypeName ? nameOfType + Name : Name + nameOfType;
            string filePath = directoryPath + name + ".cs";
            File.WriteAllText (filePath, ScriptText);
            AssetDatabase.Refresh ();
        }

        public void GenerateByKeys (Func<string> scriptNameSelcetor, params object[] keysParams)
        {
            var keys = keysParams.Select (p => (KeyValuePairStringType) p).ToDictionary (k => k.Key, k => k.Value);
            var textAsset = Resources.Load<TextAsset> (Name);

            string ScriptText = textAsset.text;

            foreach (var kvp in keys)
                ScriptText = ScriptText.Replace (kvp.Key, kvp.Value);

            if (keys.ContainsKey (namesapaceKey))
                nameOfNameSpace = keys[namesapaceKey];

            string directoryPath = Application.dataPath + "/Scripts/" + nameOfNameSpace + "/" + Name + "/";
            Directory.CreateDirectory (directoryPath);
            string filePath = directoryPath + scriptNameSelcetor () + ".cs";
            File.WriteAllText (filePath, ScriptText);
            AssetDatabase.Refresh ();
        }
    }
}
