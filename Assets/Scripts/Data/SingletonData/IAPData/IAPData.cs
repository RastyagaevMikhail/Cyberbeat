using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using OnPurchaseCompletedEvent = UnityEngine.Purchasing.IAPListener.OnPurchaseCompletedEvent;

namespace CyberBeat
{
	public class IAPData : SingletonData<IAPData>
	{
#if UNITY_EDITOR
		[UnityEditor.MenuItem ("Game/Data/IAPData")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
		public override void InitOnCreate () { CreateProducts (); }
		public override void ResetDefault ()
		{
			foreach (var productData in products)
				productData.ResetDefault ();
		}

		[ContextMenu("Create Products")]
		public void CreateProducts ()
		{
			products = new List<IAPProductData> ();
			var productCatalog = ProductCatalog.LoadDefaultCatalog ();
			foreach (var product in productCatalog.allProducts)
			{
				var productData = CreateInstance<IAPProductData> ();
				productData.InitOnCreate (product.id, product.type);
				products.Add (productData);
			}
		}
#else
		public override void ResetDefault () { }
#endif
		public List<IAPProductData> products = new List<IAPProductData> ();
		public IAPProductData this [string key]
		{
			get
			{
				return products.Find (p => p.productID == key);
			}
		}
	}
}
