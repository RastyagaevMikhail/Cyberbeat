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
			Notes.Value = int.MaxValue;
			Notes.Save ();
			DoubleNotes.Value = false;
			DoubleNotes.Save ();
		}

#endif

		[SerializeField] float GeneratedBricks = 0;
		[SerializeField] float DestroyedBricks = 0;

		public BoolVariable DoubleNotes;
		public void ActivateDoubleCoins ()
		{
			DoubleNotes.Value = true;
		}

		[SerializeField] FloatVariable ProgressCurrentLevel;
		[SerializeField] FloatVariable TotalProgressCurrentLevel;
		public Track currentTrack { get { return TracksCollection.instance.CurrentTrack; } }
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
		public void SetGeneratedBrick ()
		{
			GeneratedBricks = currentTrack.progressInfo.Max.Value;
		}
		void ProgressLevel ()
		{
			if (GeneratedBricks != 0 && DestroyedBricks != 0)
			{
				ProgressCurrentLevel.Value = (DestroyedBricks / GeneratedBricks);
				// TotalProgressCurrentLevel.Value = bestScore.
			}
		}

		[SerializeField]
		public IntVariable Notes;
		public List<TimeOfEvent> currentLines;

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

		public bool WathedRewardVideo;

		public float CurrentStartSpeed;
	}
}
