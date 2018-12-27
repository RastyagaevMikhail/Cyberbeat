﻿using System;
using System.Collections;
using System.Collections.Generic;
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
#endif
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

        public void SetLanguage (SystemLanguage language)
        {
            currentLanguage = language;
            if (OnLanguageChanged != null)
                OnLanguageChanged ();
        }

        [ContextMenu ("Parse Translations")] public void ParseTranslations ()
        {
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

        public string Localize (string str)
        {
            var result = "Undefined!";
            if (Keys.ContainsKey (str))
                result = Translations[currentLanguage][Keys[str]];
            return result;
        }
    }
    public static class LocalizatorExtention
    {
        public static string localized (this string str)
        {
            return LocalizationManager.instance.Localize (str);
        }
    }
}
