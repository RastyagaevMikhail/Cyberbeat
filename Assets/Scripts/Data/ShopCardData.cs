using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Purchasing;
namespace CyberBeat
{

	[CreateAssetMenu (fileName = "ShopCardData", menuName = "CyberBeat/Shop/Card Data", order = 0)]
	public class ShopCardData : ScriptableObject
	{
#if UNITY_EDITOR
		private const string IconPath = "Assets/Sprites/UI/Icons/Boosters/{0}.png";
		private const string CountVariablePath = "Assets/Resources/Data/Variables/GameData/{0}s.asset";
		[ContextMenu ("Init")]
		public void Init ()
		{
			Icon = Tools.GetAssetAtPath<Sprite> (IconPath.AsFormat (name));
			Count = Tools.GetAssetAtPath<IntVariable> (CountVariablePath.AsFormat (name));
			title = name.ToLower ();
			this.Save ();
		}
		public void InitOnCreate (string item_name)
		{
			Icon = Tools.GetAssetAtPath<Sprite> (IconPath.AsFormat (item_name));
			Count = Tools.GetAssetAtPath<IntVariable> (CountVariablePath.AsFormat (item_name));
			title = item_name.ToLower ();
			price = 100;
			this.CreateAsset ("Assets/Data/Shop/Boosters/{0}.asset".AsFormat (item_name));
		}
#endif
		public Sprite Icon;
		public string title;
		public string Description = "You have: {0}";
		public bool TryBuy ()
		{
			bool canBuy = GameData.instance.TryBuy (price);
			if (canBuy) Increment ();
			return canBuy;
		}
		public IntVariable Count;
		public string productID;
		public int price;
		public void Increment ()
		{
			Count.Increment ();
		}
	}
}
