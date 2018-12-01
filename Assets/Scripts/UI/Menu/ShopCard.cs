using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
namespace CyberBeat
{
	public class ShopCard : MonoBehaviour
	{
		[SerializeField] Image Icon;
		[SerializeField] LocalizeTextMeshProUGUI Title;
		[SerializeField] TextMeshProUGUI Description;
		[SerializeField] TextMeshProUGUI Price;
		[SerializeField] ShopCardData data;

		public void Buy ()
		{
			data.TryBuy ();
		}
		private void OnEnable ()
		{
			data.Count.OnValueChanged += OnCountChanged;
		}
		private void OnDisable ()
		{
			data.Count.OnValueChanged -= OnCountChanged;
		}

		private void OnCountChanged (int obj) { UpdateValues (); }

		private void Start ()
		{
			UpdateValues ();
		}
		private void UpdateValues ()
		{
			Icon.sprite = data.style.Icon;
			Description.text = data.Description.localized().AsFormat (data.Count.Value);
			Title.Id = data.title;
			Price.text = data.price.ToString ();
		}

#if UNITY_EDITOR
		[Button] public void Validate ()
		{
			foreach (var controller in GetComponentsInChildren<StyleController> ())
			{
				controller.Validate (data.style);
			}
			UpdateValues ();
			Icon.SetNativeSize ();
			name = "ShopCard_{0}".AsFormat (data.name);
			transform.localScale = Vector3.one;
		}
#endif

	}
}
