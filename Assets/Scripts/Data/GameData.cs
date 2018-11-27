using GameCore;

using Sirenix.OdinInspector;

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
			Notes.Value = 2750;
		}


#endif

		[SerializeField] float GeneratedBricks = 0;
		[SerializeField] float DestroyedBricks = 0;
		[SerializeField] IntVariable Gates;
		public void AddGates (int count)
		{
			Gates.ApplyChange (count);
		}

		[SerializeField] IntVariable Shields;

		public void AddShields (int count)
		{
			Shields.ApplyChange (count);
		}

		public BoolVariable DoubleNotes;
		public void ActivateDoubleCoins ()
		{
			DoubleNotes.Value = true;
		}

		[SerializeField] FloatVariable ProgressCurrentLevel;
		public Track currentTrack { get { return TracksCollection.instance.CurrentTrack; } }

		public void AddNotes (int notesFromAdd)
		{
			Notes.ApplyChange (notesFromAdd);
		}

		public void ResetCurrentProgress ()
		{
			GeneratedBricks = 0;
			DestroyedBricks = 0;
			ProgressCurrentLevel.Value = 0;
		}
		public void OnDestroyedBrick ()
		{
			DestroyedBricks++;
			ProgressLevel ();
		}
		public void SetGeneratedBrick (int value)
		{
			GeneratedBricks = value;
		}
		void ProgressLevel ()
		{
			if (GeneratedBricks != 0 && DestroyedBricks != 0)
				ProgressCurrentLevel.Value = 100 * (DestroyedBricks / GeneratedBricks);
		}

		[SerializeField] GameEventObject OnCantBuy;
		[SerializeField]
		public IntVariable Notes;
		public bool TryBuy (int price)
		{
			bool canBuy = CanBuy (price);
			if (canBuy)
				Notes.ApplyChange (-price);
			else
				OnCantBuy.Raise (ScriptableObject.CreateInstance<IntVariable> ().Init (price));

			return canBuy;
		}

		public bool CanBuy (int price)
		{
			bool canBuy = Notes.Value >= price;

			return canBuy;
		}
	}
}
