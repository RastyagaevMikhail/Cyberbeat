using System;

using UnityEngine;
using GameCore;
namespace CyberBeat
{

	[CreateAssetMenu (fileName = "BoosterData", menuName = "CyberBeat/BoosterData", order = 0)]
	public class BoosterData : ScriptableObject
	{
		static float[] TimeLevels = new float[] { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50 };
		public Sprite Icon;
		public RuntimeTimer timer;

		[SerializeField] IntVariable levelUpgrade;
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
			timer.Init (LifeTime);
		}
		public void Reset ()
		{
			timer.Reset ();
		}
	}
}
