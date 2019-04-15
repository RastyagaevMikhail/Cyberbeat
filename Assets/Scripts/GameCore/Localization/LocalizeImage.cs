using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    [RequireComponent (typeof (Image))]
    public class LocalizeImage : MonoBehaviour
    {
        [HideInInspector]
        [SerializeField] Image image = null;
        [SerializeField] Sprite defaultSprite;
        [SerializeField] List<LocalizedSprite> localization = null;
        [SerializeField] LocalizationManager localizator = null;

        [SerializeField] LocalizeImageSettings settings;
        private void OnValidate ()
        {
            if (image == null) image = GetComponent<Image> ();
            if (localizator == null)
                localizator = Resources.Load<LocalizationManager> ("Data/LocalizationManager");

            if (localization == null || localization != null && localization.Count == 0)
            {
                localization.Add (new LocalizedSprite () { language = SystemLanguage.English });
              //  localization.Add (new LocalizedSprite () { language = SystemLanguage.Russian });
            }
        }
        public void SetSettings (LocalizeImageSettings _settings)
        {
            settings = _settings;
        }
        private void Start ()
        {
            localizator.OnLanguageChanged += UpdateImage;
            UpdateImage ();
        }
        private void OnDestroy ()
        {
            localizator.OnLanguageChanged -= UpdateImage;
        }

        public void UpdateImage ()
        {
            settings.UpdateImage (image, localizator.currentLanguage);
        }
#if UNITY_EDITOR

        [ContextMenu ("Create Settings Asset")]
        void CreateSettingsAsset ()
        {
            settings = Tools.ValidateSO<LocalizeImageSettings> ($"Assets/Data/Localization/Image/{defaultSprite.name.Replace("_en","")}.asset");
            settings.OnCreate (defaultSprite, localization);
            settings.Save ();
        }
#endif
    }

    [System.Serializable]
    public class LocalizedSprite
    {
        public SystemLanguage language;
        public Sprite sprite;
    }
}
