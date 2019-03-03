using System.Collections.Generic;
using System.Linq;
namespace GameCore.CodeGeneration
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using System;

    using UnityEngine;

    [CreateAssetMenu (fileName = "ScriptGenerator", menuName = "GameCore/CodeGeneration/ScriptGenerator", order = 0)]
public class ScriptGenerator : ScriptableObject
    {
        [SerializeField] TextAsset template;
        [Multiline]
        [Tooltip ("Example: \"/Scripts/$NAMESPACE_NAME$/$SCRIPT_NAME$/$SCRIPT_NAME$$TYPE_NAME$.cs\"")]
        [SerializeField] string pathTemplate = "/Scripts/$NAMESPACE_NAME$/$SCRIPT_NAME$/$SCRIPT_NAME$$TYPE_NAME$.cs";
        [SerializeField] List<ScriptGenerator> dependies;
        public List<ScriptGenerator> Dependies => dependies.FindAll (d => d != this);
        [SerializeField] List<string> keys;

        public List<string> Keys => keys;

        [ContextMenu ("Parse Keys")]
        private void parseKeys ()
        {
            var matchs = Regex.Matches (template.text, @"\$[A-Z]*_[A-Z]*\$");

            keys = matchs.Cast<Match> ().Select (m => m.Value).Distinct ().ToList ();
        }
        public static List<ScriptGenerator> generatedScripts = new List<ScriptGenerator> ();
        public Dictionary<string, string> Generate (Dictionary<string, string> keySelector)
        {
            if (generatedScripts != null && generatedScripts.Contains (this))
                return null;
            else
                generatedScripts.Add (this);
            string filePath = Application.dataPath + pathTemplate;
            foreach (var keyValue in keySelector)
                filePath = filePath.Replace (keyValue.Key, keyValue.Value.Replace('.','/'));

            filePath = filePath.Replace ("$SCRIPT_NAME$", name);

            // Debug.Log ($"{filePath}");

            // bool isExist = File.Exists (filePath);

            // Debug.Log ($"{filePath} Exist {isExist}");

            // if (isExist) return null;

            Dictionary<string, string> pathTextResult = new Dictionary<string, string> ();
            string ScriptText = template.text;

            foreach (var kvp in keySelector)
                ScriptText = ScriptText.Replace (kvp.Key, kvp.Value);

            pathTextResult.Add (filePath, ScriptText);

            foreach (var scriptGenerator in Dependies)
            {
                var dict = scriptGenerator.Generate (keySelector);
                if(dict == null) continue;
                foreach (var pair in dict)
                    if (!pathTextResult.ContainsKey (pair.Key))
                        pathTextResult.Append (pair);
            }

            return pathTextResult;

            // File.WriteAllText (filePath, ScriptText);

        }
        // public List<ScriptGenerator> GetDependiosWithout (ScriptGenerator scriptGenerator)
        // {
        //     List<ScriptGenerator> result = new List<ScriptGenerator> (dependies);

        //     if (result.Contains (scriptGenerator))
        //         result.Remove (scriptGenerator);

        //     return result;
        // }

        public IEnumerable<string> GetKeysWithdependies ()
        {
            var keysofDependies = Dependies
                .SelectMany (sg => sg.Keys);
            return keys.Concat (keysofDependies).Distinct ();
        }

        [Sirenix.OdinInspector.Button] public void parseKeysByButton () => parseKeys ();
        [Sirenix.OdinInspector.Button]
        [ContextMenu ("PrintAllKeys")] void PrintAllKeys () => Debug.Log (Keys.Log ());
    }
}
