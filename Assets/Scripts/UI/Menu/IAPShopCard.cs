using GameCore;

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
		[SerializeField] bool debug;
		bool buyed { get { return productData.buyed; } }
		
		private void Awake ()
		{
			if (debug) Debug.Log ($"{this.Log()}.Awake()\n buyed =  {buyed}");
			productData.InitButton (BuyButton, UpdateData);
			UpdateData ();
		}

		public void UpdateData ()
		{
			this.DelayAction (Time.deltaTime, Validate);
		}

		void Validate ()
		{
			if (debug) Debug.Log ($"{this.Log()}.Validate()\n buyed =  {buyed}");
			MaskImage.enabled = !buyed;
			BuyButton.gameObject.SetActive (!buyed);
			Activated.SetActive (buyed);
		}

#if UNITY_EDITOR

		public void Init (IAPProductData productData)
		{
			this.productData = productData;
			name = "IAPShopCard.{0}".AsFormat (productData.productID);
			// Save In Editor on Scene
			UnityEditor.EditorUtility.SetDirty (this);
		}
#endif
	}
}
