using GameCore;

using System;

using UnityEngine;
namespace CyberBeat
{

	public abstract class BoosterData : ABayable
	{

		static float[] TimeLevels = new float[] { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50 };
		bool isActive { get { return activeBoosters.Contains (this); } }
		public int LevelUpgrade { get { return levelUpgrade.Value; } set { levelUpgrade.Value = value.GetAsClamped (0, TimeLevels.Length - 1); } }
		public float LifeTime { get { return TimeLevels[LevelUpgrade]; } }

		[Header ("Booster Data")]
		[SerializeField] float boosterUsePercent = 0.1f;
		[SerializeField] IntVariable levelUpgrade;
		[SerializeField] BoosterDataRuntimeSet activeBoosters;
		RuntimeTimer timer;
		[Header ("Events")]
		[SerializeField] UnityEventBoosterData OnReseted;
		[SerializeField] protected UnityEventBoosterData OnApply;
		[SerializeField] protected UnityEventFloat OnActivatedAsUsePrecent;

		public void InitTimer (RuntimeTimer timer)
		{
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
			// BoosterIsReseted.Raise (this);
			OnReseted.Invoke (this);
		}
		public void DeActivate ()
		{
			// Debug.LogFormat ("DeActivate.{0} {1}", this, isActive);
			if (isActive)
			{
				Reset ();
				activeBoosters.Remove (this);
			}
		}
		public void Activate ()
		{
			bool canUse = TryUse ();
			if (canUse)
			{
				InitTimer (Pool.instance.Pop<RuntimeTimer> ("RuntimeTimer"));
				activeBoosters.Add (this);

				OnActivatedAsUsePrecent.Invoke (boosterUsePercent);
			}
		}
		public abstract bool Apply (ColorBrick brick, bool equalColor = true);
	}
}
