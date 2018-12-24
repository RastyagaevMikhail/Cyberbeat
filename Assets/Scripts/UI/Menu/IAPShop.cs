using GameCore;

using Sirenix.OdinInspector;

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
		[ShowInInspector]
		public IAPData iapData { get { return IAPData.instance; } }

#if UNITY_EDITOR
		// [Button] public void CreateIAP ()
		// {
		// 	var productCatalog = ProductCatalog.LoadDefaultCatalog ();
		// 	foreach (var productAction in iapData.productActions)
		// 	{

		// 		var item = new ProductCatalogItem ();
		// 		item.type = ProductType.Consumable;
		// 		item.id = productAction.ProductID;
		// 		productCatalog.Add (item);

		// 		var writer = new System.IO.StreamWriter (ProductCatalog.kCatalogPath);
		// 		writer.Write (ProductCatalog.Serialize (productCatalog));
		// 		writer.Close ();

		// 		UnityEditor.AssetDatabase.ImportAsset (ProductCatalog.kCatalogPath);
		// 	}
		// }

		[Button] public void ValidateCards ()
		{
			var productKeys = productCards.Keys.ToList ();
			foreach (var key in productKeys)
			{
				IAPProductData productData =  iapData[key];
				productCards[key].Init(productData);
			}

		}
#endif
	}
}
