using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Purchasing;
namespace CyberBeat
{
	public class IAPShop : RectTransformObject
	{
		public Dictionary<string, Action> productActions = new Dictionary<string, Action> ();
		public Dictionary<string, IAPShopCard> productCards = new Dictionary<string, IAPShopCard> ();
		private void Awake ()
		{
			foreach (var productAction in productActions)
			{
				string key = productAction.Key;
				productCards[key].Init (productAction.Value, key == "double_notes" && gameData.DoubleNotes.Value);
			}
		}
		public void OnBuyProduct (string productID)
		{
			productActions[productID] ();
		}
		public GameData gameData { get { return GameData.instance; } }
		public void OnBuy7500Notes ()
		{
			gameData.AddNotes (7500);
		}

		public void OnBuyStarterPack ()
		{
			gameData.AddGates (10);
			gameData.AddNotes (4000);
			gameData.AddShields (5);
		}
		public void OnBuyBoxOfNotes ()
		{
			gameData.AddNotes (90000);
		}
		public void OnBuyDoubleNotes ()
		{
			gameData.ActivateDoubleCoins ();
		}
#if UNITY_EDITOR
		[Button] public void CreateIAP ()
		{
			var productCatalog = ProductCatalog.LoadDefaultCatalog ();
			foreach (var productAction in productActions)
			{

				var item = new ProductCatalogItem ();
				item.type = ProductType.Consumable;
				item.id = productAction.Key;
				productCatalog.Add (item);

				var writer = new System.IO.StreamWriter (ProductCatalog.kCatalogPath);
				writer.Write (ProductCatalog.Serialize (productCatalog));
				writer.Close ();

				UnityEditor.AssetDatabase.ImportAsset (ProductCatalog.kCatalogPath);
			}
		}

		[Button] public void ValidateCards ()
		{
			int i = 0;
			var productKeys = productActions.Keys.ToList ();
			foreach (var card in GetComponentsInChildren<IAPShopCard> ())
			{
				string productId = productKeys[i];

				card.name = "IAPCardShop_{0}".AsFormat (productId);
				card.GetComponentInChildren<IAPButton> ().productId = productId;

				productCards[productKeys[i++]] = card;
			}
		}
#endif
	}
}
