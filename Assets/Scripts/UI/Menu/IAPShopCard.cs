﻿using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace CyberBeat
{
	public class IAPShopCard : MonoBehaviour
	{
		[SerializeField] Image MaskImage;
		[SerializeField] IAPButton BuyButton;
		[SerializeField] GameObject Activated;
		[SerializeField] IAPProductData productData;
		bool buyed { get { return productData.buyed; } }
		private void OnValidate ()
		{
			MaskImage = transform.Find ("BackPadMask").GetComponent<Image> ();
			BuyButton = transform.Find ("BuyButton").GetComponent<IAPButton> ();
			Activated = transform.Find ("Activated").gameObject;
		}
		private void Awake ()
		{
			productData.InitButton (BuyButton, UpdateData);
			UpdateData ();
		}

		private void UpdateData ()
		{
			Tools.DelayAction (this,
				Time.deltaTime, () =>
				{
					MaskImage.enabled = !buyed;
					BuyButton.gameObject.SetActive (!buyed);
					Activated.SetActive (buyed);
				});
		}

		public void Init (IAPProductData productData)
		{
			this.productData = productData;
			name = "IAPShopCard.{0}".AsFormat (productData.productID);
		}
	}
}
