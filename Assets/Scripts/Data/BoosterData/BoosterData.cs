using GameCore;

using System;

using UnityEngine;
namespace CyberBeat
{

	public abstract class BoosterData : ScriptableObject
	{
		static float[] TimeLevels = new float[] { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50 };
		public Sprite Icon;
		public RuntimeTimer timer;
		public BoostersData data { get { return BoostersData.instance; } }

		[SerializeField] IntVariable levelUpgrade;
		[SerializeField] IntVariable count;
		[SerializeField] GameEventBoosterData BoosterIsOver;
		bool isActive { get { return data.ActiveBoosters.Contains (this); } }
		public int LevelUpgrade { get { return levelUpgrade.Value; } set { levelUpgrade.Value = value.GetAsClamped (0, TimeLevels.Length - 1); } }

		public float LifeTime
		{
			get
			{
				return TimeLevels[LevelUpgrade];
			}
		}

		public void InitTimer (RuntimeTimer timer)
		{
			this.timer = timer;
			timer.OnTimeElapsed += DeActivate;
			timer.Init (LifeTime);

		}
		public void Reset ()
		{
			timer.Reset ();
			timer.OnTimeElapsed -= DeActivate;
			timer = null;
		}

		public void DeActivate ()
		{
			if (isActive)
			{
				data.ActiveBoosters.Remove (this);
			}
		}

		public void Activate ()
		{

			if (!isActive && count.Value > 0)
			{
				data.ActiveBoosters.Add (this);
				count.Decrement ();
			}
			else if (count.Value == 0)
			{
				BoosterIsOver.Raise (this);
			}
		}
		public abstract void Apply (ColorBrick brick);

	}
}
