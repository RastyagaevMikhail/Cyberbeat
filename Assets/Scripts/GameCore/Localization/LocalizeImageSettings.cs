using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
	[CreateAssetMenu (fileName = "LocalizationImageSettings", menuName = "GameCore/Localization/Image/Settings")]
	public class LocalizeImageSettings : ScriptableObject
	{
		[SerializeField] Sprite defaultSprite;
		[SerializeField] List<LocalizedSprite> localization = null;
		private void OnValidate ()
		{

			if (localization == null || localization != null && localization.Count == 0)
			{
				localization.Add (new LocalizedSprite () { language = SystemLanguage.English });
				localization.Add (new LocalizedSprite () { language = SystemLanguage.Russian });
			}

		}
		public void UpdateImage (Image image, SystemLanguage currentLanguage)
		{
			if (!image) return;

			var locSprtite = localization.Find (item => item.language == currentLanguage);
			image.sprite = (locSprtite != null) ? locSprtite.sprite : defaultSprite;
			image.SetNativeSize ();
		}

		public void OnCreate (Sprite _defaultSprite, List<LocalizedSprite> _localization)
		{
			defaultSprite = _defaultSprite;
			localization = _localization;
		}

	}
}
