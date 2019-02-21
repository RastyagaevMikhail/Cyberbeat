using GameCore;

using System;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class GameData : SingletonData<GameData>
	{
#if UNITY_EDITOR
		[UnityEditor.MenuItem ("Game/Data/GameData")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
		public override void InitOnCreate ()
		{

		}
		public override void ResetDefault ()
		{
			Notes.ResetDefault();
			Notes.Save ();
			DoubleNotes.Value = false;
			DoubleNotes.Save ();
		}

#endif

		public BoolVariable DoubleNotes;
	

		[SerializeField]
		public IntVariable Notes;


		public bool TryBuy (int price)
		{
			bool canBuy = CanBuy (price);
			if (canBuy)
				Notes.ApplyChange (-price);

			return canBuy;
		}

		public bool CanBuy (int price)
		{
			bool canBuy = Notes.Value >= price;

			return canBuy;
		}

	}
}
