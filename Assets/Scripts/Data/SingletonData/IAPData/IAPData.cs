using GameCore;

using Sirenix.OdinInspector;

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
		public override void InitOnCreate () { }
		public override void ResetDefault ()
		{
			foreach (var productData in products)
				productData.ResetDefault ();
		}

		[Button] public void CreatProducts ()
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

		[Button] public void ValidateProductActionsDictionary ()
		{
			productActions = new List<ProductAction> ();
			var productCatalog = ProductCatalog.LoadDefaultCatalog ();
			foreach (var product in productCatalog.allProducts)
			{
				productActions.Add (new ProductAction (product.id));
			}
		}

#endif
		public GameData gameData { get { return GameData.instance; } }
		public AdsController ads { get { return AdsController.instance; } }
		public List<ProductAction> productActions = new List<ProductAction> ();
		public List<IAPProductData> products = new List<IAPProductData> ();
		public IAPProductData this [string key]
		{
			get
			{
				return products.Find (p => p.productID == key);
			}
		}
		//* ---Nonconsumable----  */
		public void OnBuyNoAds (Product product)
		{
			ads.ActivateNoAds ();
		}
		public void OnBuyDoubleNotes (Product product)
		{
			gameData.ActivateDoubleCoins ();
		}
		//* */

		//* ---Consumable----  */
		public void OnBuy7500Notes (Product product)
		{
			gameData.AddNotes (7500);
		}

		public void OnBuyStarterPack (Product product)
		{
			gameData.AddGates (10);
			gameData.AddNotes (4000);
			gameData.AddShields (5);
		}
		public void OnBuyBoxOfNotes (Product product)
		{
			gameData.AddNotes (90000);
		}
		//* */

	}

	[Serializable]
	public class ProductAction
	{
		[HorizontalGroup]
		[HideLabel]
		public string ProductID;
		[HorizontalGroup]
		[DrawWithUnity]
		public OnPurchaseCompletedEvent action;
		public ProductAction (string id)
		{
			ProductID = id;
		}
	}
}
