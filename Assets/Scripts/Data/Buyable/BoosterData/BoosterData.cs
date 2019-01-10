using GameCore;

using System;

using UnityEngine;
namespace CyberBeat
{

	public abstract class BoosterData : ABayable
	{
		[ContextMenu ("Valiadte")]
		void Valiadte ()
		{
			Icon = shopData.Icon;
			Description = description;
			title = name.ToLower ();
			Count = shopData.Count;
			this.Save ();
		}
		static float[] TimeLevels = new float[] { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50 };
		[Header ("Booster Data")]
		public RuntimeTimer timer;
		public BoostersData data { get { return BoostersData.instance; } }
		public ShopCardData shopData;
		[SerializeField] IntVariable levelUpgrade;
		[SerializeField] GameEventBoosterData BoosterIsActivated;
		[SerializeField] GameEventBoosterData BoosterIsReseted;

		public float boosterUsePercent = 0.1f;
		public string description;
		bool isActive { get { return data.ActiveBoosters.Contains (this); } }
		public int LevelUpgrade { get { return levelUpgrade.Value; } set { levelUpgrade.Value = value.GetAsClamped (0, TimeLevels.Length - 1); } }
		public float LifeTime
		{
			get
			{
				if (Count.Value == 0) return 0f;
				return TimeLevels[LevelUpgrade];
			}
		}
		public void InitTimer (RuntimeTimer timer)
		{
			if (LifeTime == 0)
			{
				DeActivate ();
				return;
			}
			this.timer = timer;
			timer.OnTimeElapsed += DeActivate;
			timer.Init (LifeTime);
		}
		public void Reset ()
		{
			if (timer)
			{
				timer.OnTimeElapsed -= DeActivate;
				timer.Reset ();
				timer = null;
			}
			BoosterIsReseted.Raise (this);
		}
		public void DeActivate ()
		{
			if (isActive)
			{
				// Debug.Log ("DeActivate  {0}".AsFormat (name));
				Reset ();
				data.DeActivate (this);
			}
		}
		public void Activate ()
		{
			if (TryUse ())
			{
				InitTimer (Pool.instance.Pop<RuntimeTimer> ("RuntimeTimer"));
				data.ActiveBoosters.Add (this);
				BoosterIsActivated.Raise (this);

			}
		}
		public abstract void Apply (ColorBrick brick);
	}
}
