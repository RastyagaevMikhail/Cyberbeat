using System.Collections;
using System.Collections.Generic;
using System.IO;

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

        const string namesapaceKey = "$NAMESPACE_NAME$";
        const string typeKey = "$TYPE_NAME$";

        [MenuItem ("Game/Generation/Script")]
        static void CreateWizard ()
        {
            ScriptableWizard.DisplayWizard<ScriptGenerationWizard> ("Create Scripts", "Generate");
        }

        void OnWizardCreate ()
        {
            if (runtimeSet)
            {
                ScritpGenerator RuntimeSetScriptGenerator = new ScritpGenerator ()
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
                ScritpGenerator RuntimeSetScriptGenerator = new ScritpGenerator ()
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
                ScritpGenerator GameEventScriptGenerator = new ScritpGenerator ()
                {
                    Name = "GameEvent",
                    nameOfType = nameOfType,
                    typeKey = typeKey,
                    namesapaceKey = namesapaceKey,
                    nameOfNameSpace = nameOfNameSpace
                };
                GameEventScriptGenerator.Generate (false);

                ScritpGenerator GameEventListenerScriptGenerator = new ScritpGenerator ()
                {
                    Name = "GameEventListener",
                    nameOfType = nameOfType,
                    typeKey = typeKey,
                    namesapaceKey = namesapaceKey,
                    nameOfNameSpace = nameOfNameSpace
                };
                GameEventListenerScriptGenerator.Generate (false);

            }
        }

        void OnWizardUpdate ()
        {
            helpString = "Chose the Script type generation";
        }

    }

    public class ScritpGenerator
    {
        public string Name;
        public string nameOfType;
        public string typeKey;
        public string namesapaceKey;
        public string nameOfNameSpace;

        public void Generate (bool ForwardNameTypte = true)
        {
            var textAsset = Resources.Load<TextAsset> (Name);

            string tempateText = textAsset.text;

            string ScriptText = tempateText.Replace (namesapaceKey, nameOfNameSpace);
            ScriptText = ScriptText.Replace (typeKey, nameOfType);

            string directoryPath = Application.dataPath + "/Scripts/" + nameOfNameSpace + "/" + Name + "/";
            Directory.CreateDirectory (directoryPath);
            string name = ForwardNameTypte ? nameOfType + Name : Name + nameOfType;
            string filePath = directoryPath + name + ".cs";
            File.WriteAllText (filePath, ScriptText);
            AssetDatabase.Refresh ();
        }
    }
}
