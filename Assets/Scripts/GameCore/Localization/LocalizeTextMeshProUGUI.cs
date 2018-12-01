using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
namespace GameCore
{
    [ExecuteInEditMode]
    [RequireComponent (typeof (TextMeshProUGUI))]
    public class LocalizeTextMeshProUGUI : MonoBehaviour
    {
        private TextMeshProUGUI mText;
        [InlineButton ("RemoveID")]
        [ValueDropdown ("IDs")]
        public string Id;
        public List<string> IDs
        {
            get { return Localizator.GetTranslations ().Select (t => t.code).ToList (); }
        }
#if UNITY_EDITOR
        public void RemoveID ()
        {
            Localizator.RemoveTranslation (Id);
        }
#endif
        public enum CaseType
        {
            Normal,
            Uppercase,
            Capitalize,
            Lowercase
        }

        public CaseType letters = CaseType.Normal;

#if UNITY_EDITOR
        private Coroutine mCoroutine = null;
        IEnumerator UpdateCoroutine ()
        {
            while (true)
            {
                yield return new WaitForSeconds (0.2f);
                UpdateText ();
            }
        }

#endif

        void Awake ()
        {
            Localizator.OnLanguageChanged += () =>
            {
                if (this != null)
                {
                    UpdateText ();
#if UNITY_EDITOR
                    if (this != null)
                    {
                        UnityEditor.EditorUtility.SetDirty (this);
                    }
#endif
                }
            };
        }

        void OnEnable ()
        {
            UpdateText ();

#if UNITY_EDITOR
            if (mCoroutine == null)
            {
                mCoroutine = StartCoroutine (UpdateCoroutine ());
            }
#endif
        }

        public void UpdateText ()
        {
            if (this == null)
            {
                return;
            }
            if (mText == null)
            {
                mText = GetComponent<TextMeshProUGUI> ();
            }
            if (mText != null)
            {
                string str = Localizator.s (Id);
                if (string.IsNullOrEmpty (str))
                {
                    str = "Undefined!";
                }

                switch (letters)
                {
                    case CaseType.Uppercase:
                        str = str.ToUpper ();
                        break;
                    case CaseType.Lowercase:
                        str = str.ToLower ();
                        break;
                    case CaseType.Capitalize:
                        str = FirstLetterToUpper (str);
                        break;
                }

                mText.text = str;
            }
        }

        public static string FirstLetterToUpper (string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper (str[0]) + str.Substring (1);

            return str.ToUpper ();
        }
    }
}
