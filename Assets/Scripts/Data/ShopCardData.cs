using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Purchasing;
using GameCore;
namespace CyberBeat
{

	[CreateAssetMenu (fileName = "ShopCardData", menuName = "Color Bricks/Shop/Card Data", order = 0)]
	public class ShopCardData : ScriptableObject
	{
        public ColorsStyle style;
		public Sprite Icon;
		public string title;
		public string Description = "You have: {0}";
		public bool isIAP;

		public void TryBuy ()
		{
			bool canBuy = GameData.instance.TryBuy (price);
			if (canBuy) Count.Increment ();
		}

		public IntVariable Count;
		[ShowIf ("isIAP")]
		public string productID;
		// [ShowIf ("isIAP")]
		// public ProductType productType;
		[HideIf ("isIAP")]
		public int price;
#if UNITY_EDITOR
        private const string IconPath = "Assets/Sprites/UI/Icons/Boosters/{0}.png";
        private const string CountVariablePath = "Assets/Resources/Data/Variables/GameData/{0}s.asset";

        [Button] public void Init ()
		{
			style = Tools.GetAssetAtPath<ColorsStyle> ("Assets/Data/ColorsStyles/{0}Style.asset".AsFormat (name));
			Icon = Tools.GetAssetAtPath<Sprite> (IconPath.AsFormat (name));
			Count = Tools.GetAssetAtPath<IntVariable> (CountVariablePath.AsFormat (name));
			title = name.ToLower ();
			this.Save ();
		}
		public void InitOnCreate (string item_name)
		{
			style = Tools.GetAssetAtPath<ColorsStyle> ("Assets/Data/ColorsStyles/{0}Style.asset".AsFormat (item_name));
			Icon = Tools.GetAssetAtPath<Sprite> (IconPath.AsFormat (item_name));
			Count = Tools.GetAssetAtPath<IntVariable> (CountVariablePath.AsFormat (item_name));
			title = item_name.ToLower ();
			price = 100;
			this.CreateAsset ("Assets/Data/Shop/Boosters/{0}.asset".AsFormat (item_name));
		}
#endif
	}
}
