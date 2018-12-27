using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Text = TMPro.TextMeshProUGUI;

using UnityEngine;
namespace GameCore
{
    [ExecuteInEditMode]
    [RequireComponent (typeof (Text))]
    public class LocalizeTextMeshProUGUI : MonoBehaviour
    {
        public LocalizationManager localizator { get { return LocalizationManager.instance; } }
        private Text _mText = null;
        public Text mText { get { if (_mText == null) _mText = GetComponent<Text> (); return _mText; } }

        public string Id;

        void OnEnable ()
        {
            UpdateText ();
            localizator.OnLanguageChanged += UpdateText;
        }
        private void OnDisable ()
        {
            localizator.OnLanguageChanged -= UpdateText;
        }

        [ContextMenu ("Update Text")]
        public void UpdateText ()
        {
            mText.text = Id.localized ();
#if UNITY_EDITOR
            if (this != null)
            {
                UnityEditor.EditorUtility.SetDirty (this);
            }
#endif
        }
    }
}
