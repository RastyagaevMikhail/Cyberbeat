using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

namespace GameCore
{
	public class ContentButton : MonoBehaviour
	{
		private Button _button = null;
		public Button button { get { if (_button == null) _button = GetComponent<Button> (); return _button; } }
		public bool interactable { get { return button.interactable; } set { button.interactable = value; } }
		public Button.ButtonClickedEvent onClick { get { return button.onClick; } set { button.onClick = value; } }
		public void SetActive (bool active)
		{
			gameObject.SetActive (active);
		}
		public Graphic targetGraphic { get { return button.targetGraphic; } }
		public Text textCompnent;

		public string text { get { return textCompnent.text; } set { textCompnent.text = value; } }
		private LocalizeTextMeshProUGUI _localText = null;
		public LocalizeTextMeshProUGUI localText { get { if (_localText == null) _localText = textCompnent.GetComponent<LocalizeTextMeshProUGUI> (); return _localText; } }
		public string textLocalizationID { get { return localText.ID; } set { localText.SetID (value); } }
		public Color textColor { get { return textCompnent.color; } set { textCompnent.color = value; } }
	}
}
