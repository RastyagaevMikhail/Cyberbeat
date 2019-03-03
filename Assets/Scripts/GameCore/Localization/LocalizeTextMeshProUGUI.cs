using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Text = TMPro.TextMeshProUGUI;

using Sirenix.OdinInspector;

using UnityEngine;
using System;

namespace GameCore
{
    [ExecuteInEditMode]
    [RequireComponent (typeof (Text))]
    public class LocalizeTextMeshProUGUI : MonoBehaviour
    {
        [SerializeField]
        [InlineButton ("Parse", "P")]
        LocalizationManager localizator;
        [HideInInspector][SerializeField] Text mText;
        private void OnValidate ()
        {
            if (localizator == null) localizator = Resources.Load<LocalizationManager> ("Data/LocalizationManager");
            if (mText == null) mText = GetComponent<Text> ();
        }
        [SerializeField] string Id;
        public string ID => Id;
        [SerializeField] bool debug;
        void OnEnable ()
        {
            UpdateText ();
            localizator.OnLanguageChanged += UpdateText;
        }
        private void OnDisable ()
        {
            localizator.OnLanguageChanged -= UpdateText;
        }
        private void Update ()
        {
            UpdateText ();
        }
        private void Parse() {
            localizator.ParseTranslations();
        }

        [ContextMenu ("Update Text")]
        private void UpdateText ()
        {
#if UNITY_EDITOR
            if (this != null)
            {
                UnityEditor.EditorUtility.SetDirty (this);
            }
#endif
            mText.text = localizator.Localize (Id, debug);
        }

		public void SetID(string localizationID)
		{
			Id = localizationID;
            UpdateText();
		}
	}
}
