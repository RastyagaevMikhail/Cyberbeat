using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;

namespace GameCore
{
    public class LocalizeWindow : OdinEditorWindow
    {
        public List<Localizator.Trans> rows;
        Vector2 scrollPos;
        public Localizator localizator { get { return Localizator.instance; } }

        [MenuItem (Localizator.LocalizeWindowMenuPath)]
        public static void ShowWindow ()
        {
            LocalizeWindow localizeWindow = GetWindow<LocalizeWindow> ();
            localizeWindow.Show ();
            localizeWindow.rows = Localizator.instance.GetTranslations ().ToList ();
            // EditorWindow.GetWindowWithRect (typeof (LocalizeWindow),
            //     new Rect (0, 0, 600, 400), false, "Localizations");
        }
        // void OnGUI ()
        // {
        //     var translations = localizator.GetTranslations ();
        //     GUILayout.BeginHorizontal ();
        //     GUILayout.Label ("", GUILayout.Width (180));
        //     for (int j = 0; j < localizator.Languages.Length; j++)
        //     {
        //         GUILayout.Label (localizator.Languages[j].ToString (), GUILayout.Width (150));
        //     }
        //     GUILayout.EndHorizontal ();

        //     using (var h = new EditorGUILayout.HorizontalScope ())
        //     {
        //         using (var scrollView = new EditorGUILayout.ScrollViewScope (scrollPos))
        //         {
        //             scrollPos = scrollView.scrollPosition;
        //             //GUILayout.BeginScrollView(Vector2.zero);
        //             for (int i = 0; i < translations.Length; i++)
        //             {
        //                 GUILayout.BeginHorizontal ();

        //                 if (GUILayout.Button ("X", GUILayout.Width (30)))
        //                 {
        //                     localizator.RemoveTranslation (translations[i].code);
        //                     EditorUtility.SetDirty (this);
        //                 }
        //                 var transCode = EditorGUILayout.TextField (translations[i].code, GUILayout.Width (150));
        //                 if (transCode.Equals (translations[i].code) == false)
        //                 {
        //                     localizator.ChangeKey (translations[i].code, transCode);
        //                     EditorUtility.SetDirty (this);
        //                 }
        //                 for (int j = 0; j < translations[i].val.Length; j++)
        //                 {
        //                     var transValues = EditorGUILayout.TextField (translations[i].val[j], GUILayout.Width (150));
        //                     if (transValues.Equals (translations[i].val[j]) == false)
        //                     {
        //                         localizator.SaveLocalization (translations[i].code, j, transValues, false);

        //                     }
        //                 }
        //                 if (GUILayout.Button ("Write To File"))
        //                 {
        //                     localizator.WriteToFile ();
        //                     EditorUtility.SetDirty (this);
        //                 }

        //                 GUILayout.EndHorizontal ();
        //             }

        //             if (GUILayout.Button ("Add translation"))
        //             {
        //                 localizator.AddTranslation ("transl");
        //                 EditorUtility.SetDirty (this);
        //             }
        //         }
        //     }
        //     //   GUILayout.EndScrollView();
        // }

        [System.Serializable]
        public class TranslationRow
        {
            public string code;
            public List<string> translations;
        }
    }
}
