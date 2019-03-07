using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

using UnityEditor;

using UnityEngine;
namespace GameCore.CodeGeneration
{
    public class ScriptGenerationWizard : ScriptableWizard
    {
        [SerializeField] ScriptGenerator scriptGenerator;
        [SerializeField] KeyValuePairStringType[] KeysValues;

        [MenuItem ("Game/Generation/Script")]
        static void CreateWizard ()
        {
            var wizard = ScriptableWizard.DisplayWizard<ScriptGenerationWizard> ("Create Scripts", "Generate", "Get keys");
        }

        void OnWizardCreate ()
        {
            if (scriptGenerator)
            {
                Dictionary<string, string> keySelector = KeysValues.ToDictionary (kv => kv.Key, kv => kv.Value);

                if (ScriptGenerator.generatedScripts == null)
                    ScriptGenerator.generatedScripts = new List<ScriptGenerator> ();
                else
                    ScriptGenerator.generatedScripts.Clear ();

                var pahtText = scriptGenerator.Generate (keySelector);

                ScriptGenerator.generatedScripts.Clear ();

                Debug.Log (pahtText.Log ());

                foreach (var pair in pahtText)
                {
                    Tools.ValidatePath (
                        Path.GetDirectoryName ("Assets" + pair.Key.Replace (Application.dataPath, string.Empty)).Replace ('\\', '/')
                    );

                    if (File.Exists (pair.Key))
                    {
                        Debug.Log ($"File {pair.Key} Is Exist");
                        continue;
                    }

                    File.WriteAllText (pair.Key, pair.Value);

                    AssetDatabase.Refresh ();
                }
            }
        }

        void OnWizardUpdate ()
        {
            if (!scriptGenerator)
            {
                errorString = "Choose the Script type generation";
                helpString = string.Empty;
                isValid = false;
            }
            else
            if (KeysIsNullOrEmpty)
            {
                errorString = "Press Get keys";
                helpString = string.Empty;
                isValid = true;
            }
            // else
            // if (ValuesIsEmpty)
            // {
            //     errorString = "Fill the keys.";
            //     helpString = string.Empty;
            //     isValid = false;
            // }
            else
            {
                errorString = string.Empty;
                helpString = "Ready To Generate. Press to \"Generate\" ";
                isValid = true;
            }
        }

        // private bool ValuesIsEmpty => !KeysIsNullOrEmpty && KeysValues.ToList ().Any (kv => kv.Value == string.Empty);

        private bool KeysIsNullOrEmpty => KeysValues == null || KeysValues.Length == 0;

        void OnWizardOtherButton ()
        {
            if (!scriptGenerator) return;

            KeysValues = scriptGenerator
                .GetKeysWithdependies ()
                .Select (k => new KeyValuePairStringType (k, string.Empty))
                .ToArray ();
        }
    }

    [Serializable]
    public class KeyValuePairStringType
    {
        [SerializeField] string key;
        [SerializeField] string value;

        public string Key => key;
        public string Value => value;

        public KeyValuePairStringType (string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public KeyValuePairStringType ()
        {
            key = "$_$";
            value = string.Empty;
        }
    }

    [CustomPropertyDrawer (typeof (KeyValuePairStringType))]
    public class KeyValuePairStringTypeDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var key = property.FindPropertyRelative ("key");
            var value = property.FindPropertyRelative ("value");
            var rect = EditorGUI.PrefixLabel (position, new GUIContent (key.stringValue));
            EditorGUI.PropertyField (rect, value, GUIContent.none);
        }
    }

}
