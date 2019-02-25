using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

namespace GameCore
{
    public class LocalizationManager : SingletonData<LocalizationManager>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem ("Game/Data/LocalizationManager")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
        public override void InitOnCreate () { ParseTranslations (); }
        public override void ResetDefault () { }
#else
        public override void ResetDefault () { }
#endif
        private const string TranslationPathFolder = "Assets/Resources/";
        private const string TranslationsFilename = "translations.csv";
        private Dictionary<SystemLanguage, string> UndefinedTranslations = new Dictionary<SystemLanguage, string>
        { //
            { SystemLanguage.English, "Undefined Translations!!!" },
            { SystemLanguage.Russian, "Перевод не найден" }
        };
        string undefinedTranslation
        {
            get
            {
                string result = String.Empty;
                UndefinedTranslations.TryGetValue (currentLanguage, out result);
                if (result == string.Empty) return "Undefined Translations!!!";
                return result;
            }
        }

        [Serializable]
        public class LanguageItems
        {
            public SystemLanguage Lang;
            public List<string> Items;
        }

        [Serializable]
        public class LocalizationKey
        {
            public string key;
            public int index;
        }
        public List<LocalizationKey> keys;
        public List<LanguageItems> translations;

        [NonSerialized]
        public Dictionary<string, int> _keys = null;
        public Dictionary<string, int> Keys
        {
            get
            {
                if (_keys == null)
                    _keys = keys.ToDictionary (key => key.key, key => key.index);
                return _keys;
            }
        }

        [NonSerialized]
        public Dictionary<SystemLanguage, List<string>> _translations = null;
        public Dictionary<SystemLanguage, List<string>> Translations
        {
            get
            {
                if (_translations == null)
                    _translations = translations.ToDictionary (trans => trans.Lang, trans => trans.Items);
                return _translations;
            }
        }
        public List<SystemLanguage> Languges;
        [NonSerialized]
        public Action OnLanguageChanged;
        public SystemLanguage currentLanguage
        {
            get
            {
                string langStr = PlayerPrefs.GetString ("Language", Application.systemLanguage.ToString ());
                return (SystemLanguage) Enum.Parse (typeof (SystemLanguage), langStr);
            }
            set
            {
                PlayerPrefs.SetString ("Language", value.ToString ());
            }
        }

        // [SerializeField] bool debug;
        [Button]
        public void SetLanguage (SystemLanguage language)
        {
            currentLanguage = language;
            if (OnLanguageChanged != null)
                OnLanguageChanged ();
        }
        [Button]
        [ContextMenu ("Parse Translations")]
        public void ParseTranslations ()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            #endif
            ParseCSV (Resources.Load<TextAsset> ("translations"));
        }
        public void ParseCSV (TextAsset file)
        {
            string[] lines = file.text.Split ('\n');
            if (lines.Length == 0)
            {
                Debug.LogErrorFormat ("file {0} is empty", file);
                return;
            }

            Languges = new List<SystemLanguage> ();

            string[] line = lines[0].Trim ().Split ('\t');

            for (int i = 1; i < line.Length; i++)
            {
                var lang = Enum.Parse (typeof (SystemLanguage), line[i]);
                Languges.Add ((SystemLanguage) lang);
            }

            keys = new List<LocalizationKey> ();
            translations = new List<LanguageItems> ();
            // List<string>  currentLanguage = new List<string>();
            for (int i = 1; i < lines.Length; i++)
            {
                line = lines[i].Trim ().Split ('\t');
                keys.Add (new LocalizationKey () { key = line[0], index = i - 1 });
                for (int l = 1; l < line.Length; l++)
                {
                    SystemLanguage currentLanguage = Languges[l - 1];
                    if (!translations.Exists (t => t.Lang == currentLanguage))
                        translations.Add (new LanguageItems () { Lang = currentLanguage, Items = new List<string> () { line[l] } });
                    else
                        translations.Find (t => t.Lang == currentLanguage).Items.Add (line[l]);
                }
            }
        }

#if UNITY_EDITOR

        [ContextMenu ("Save Translations")]
        void SaveTranslations ()
        {
            string path = TranslationPathFolder + TranslationsFilename;
            if (!File.Exists (path))
            {
                Debug.LogError ("Unable to save translations to file " + path + ". Make sure that file exists.");
                return;
            }

            using (FileStream fs = new FileStream (path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter (fs))
                {
                    writer.Write ("#	");
                    for (int i = 0; i < Languges.Count; i++)
                    {
                        writer.Write (Languges[i].ToString () + "	");
                    }
                    writer.Write (System.Environment.NewLine);
                    for (int i = 0; i < keys.Count; i++)
                    {
                        LocalizationKey key = keys[i];
                        writer.Write (key.key + "	");
                        for (int j = 0; j < Languges.Count; j++)
                        {
                            var lang = Languges[j];
                            string Transl = Translations[lang][key.index];
                            if (Transl == null)
                            {
                                Transl = "";
                            }
                            writer.Write (Transl.Replace ("\n", "<br>") + "	");
                        }
                        writer.Write (System.Environment.NewLine);
                    }
                }
            }
            UnityEditor.AssetDatabase.Refresh ();
        }
#endif

        public string Localize (string str, bool debug = false)
        {
            if (debug) Debug.LogFormat ("str = {0}", str);
            if (String.IsNullOrEmpty (str)) return undefinedTranslation;
            //? try get list transations
            List<string> translations = null;
            Translations.TryGetValue (currentLanguage, out translations);
            if (debug) Debug.Log (translations.Log ());
            if (translations == null) return undefinedTranslation;

            //? try get index of key from transation
            int indexOfTranslation = -1;
            Keys.TryGetValue (str, out indexOfTranslation);
            if (debug) Debug.LogFormat ("indexOfTranslation = {0}", indexOfTranslation);
            if (indexOfTranslation == 0 && str != keys[0].key) return undefinedTranslation;
            if (debug) Debug.LogFormat ("translations[indexOfTranslation] = {0}", translations[indexOfTranslation]);
            return translations[indexOfTranslation];
        }
    }
    public static class LocalizatorExtention
    {
        public static string localized (this string str, bool debug = false)
        {
            return LocalizationManager.instance.Localize (str, debug);
        }
    }
}
