using GameCore;

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
		[SerializeField] IntVariableTextSetter CountSetter;
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
			Icon.sprite = data.Icon;
			Title.Id = data.title;
			Price.text = data.price.ToString ();
		}

#if UNITY_EDITOR
		[ContextMenu ("Validate")]
		public void Validate ()
		{
			UpdateValues ();
			Icon.SetNativeSize ();
			CountSetter.SetVariavle (data.Count);
			name = "ShopCard_{0}".AsFormat (data.name);
			transform.localScale = Vector3.one;
		}
#endif

	}
}
