using GameCore;

using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
	public class RandomConstantColors
	{
		private readonly RandomStack<Color> randStack;
		public Color Constant;
		private List<Color> RandomSet;

		public RandomConstantColors ()
		{
			Constant = Colors.instance.RandomColor;
			RandomSet = new List<Color> (Colors.instance.colors);
			if (RandomSet.Count > 1)
				RandomSet.Remove (Constant);
			randStack = new RandomStack<Color> (RandomSet);
		}

		public Color Random { get { return randStack.Get (); } }
	}
}
