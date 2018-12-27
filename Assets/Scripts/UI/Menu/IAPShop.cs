using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
namespace CyberBeat
{
	public class IAPShop : RectTransformObject
	{
		public Dictionary<string, IAPShopCard> productCards = new Dictionary<string, IAPShopCard> ();
		public IAPData iapData { get { return IAPData.instance; } }

#if UNITY_EDITOR

		[ContextMenu ("Validate Cards")]
		public void ValidateCards ()
		{
			var productKeys = productCards.Keys.ToList ();
			foreach (var key in productKeys)
			{
				IAPProductData productData = iapData[key];
				productCards[key].Init (productData);
			}

		}
#endif
	}
}
