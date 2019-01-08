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
		public ShopCardData shopData;
		[SerializeField] IntVariable levelUpgrade;
		[SerializeField] IntVariable count;
		[SerializeField] GameEventBoosterData BoosterIsActivated;
		[SerializeField] GameEventBoosterData BoosterIsReseted;
		[SerializeField] GameEventBoosterData BoosterIsOver;
		public float boosterUsePercent = 0.1f;
		public string description;
		bool isActive { get { return data.ActiveBoosters.Contains (this); } }
		public int LevelUpgrade { get { return levelUpgrade.Value; } set { levelUpgrade.Value = value.GetAsClamped (0, TimeLevels.Length - 1); } }
		public float LifeTime
		{
			get
			{
				if (count.Value == 0) return 0f;
				return TimeLevels[LevelUpgrade];
			}
		}
		public int Price { get { return shopData.price; } }
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
			if (!isActive && count.Value > 0)
			{
				data.ActiveBoosters.Add (this);

				InitTimer (Pool.instance.Pop<RuntimeTimer> ("RuntimeTimer"));

				count.Decrement ();

				BoosterIsActivated.Raise (this);
			}
			else if (count.Value == 0)
			{
				BoosterIsOver.Raise (this);
			}
		}
		public abstract void Apply (ColorBrick brick);
		public bool TryBuy ()
		{
			return shopData.TryBuy ();
		}
		public void Increment ()
		{
			shopData.Increment ();
		}
	}
}
