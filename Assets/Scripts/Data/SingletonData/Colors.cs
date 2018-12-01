using GameCore;

using Sirenix.OdinInspector;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{

	public class Colors : SingletonData<Colors>
	{
#if UNITY_EDITOR
		[UnityEditor.MenuItem ("Game/Data/Colors")] public static void Select () { UnityEditor.Selection.activeObject = instance; }

		[Button]
		public override void InitOnCreate () { GenerateColorsCount (); }
		void GenerateColorsCount ()
		{
			ColorsCounter = new Dictionary<Color, ColorCountVariable> ();
			foreach (var clr in AllColors)
			{
				var colorCountVar = ScriptableObject.CreateInstance<ColorCountVariable> ();
				colorCountVar.color = clr;
				colorCountVar.name = "#{0}".AsFormat (clr.ToString (false));
				ColorsCounter.Add (clr, colorCountVar);
				Tools.CreateAsset (colorCountVar, "Assets/Resources/Data/Variables/Colors/{0}.asset".AsFormat (colorCountVar.name));
			}
			this.Save ();
		}
		public override void ResetDefault ()
		{
			levelOnColorProgress.Value = 0;
		}
#endif
		//---Colors---
		[ShowInInspector] private List<Color> _colors = null;
		public List<Color> colors
		{
			get
			{
				if (_colors == null || _colors.Count - 1 != levelOnColorProgress.Value)
				{
					_colors = new List<Color> ();
					for (int i = 0; i < levelOnColorProgress.Value + 1; i++)
						_colors.Add (AllColors[i]);
				}
				return _colors;
			}
		}

		// ;
		public List<Color> AllColors;
		int[] colorsByLevel = new int[] { 50, 100, 50, 150, 75, 50, 200 };
		[SerializeField] IntVariable levelOnColorProgress;
		public int LevelOnColorProgress { get { return levelOnColorProgress.Value; } set { levelOnColorProgress.Value = value; } }

		public int ColorsPerCell { get { return colorsByLevel.InRangeIndex (levelOnColorProgress.Value) ? colorsByLevel[levelOnColorProgress.Value] : 50; } }

		public Dictionary<Color, ColorCountVariable> ColorsCounter = new Dictionary<Color, ColorCountVariable> ();
		public bool ColorsCounterIsFull { get { return ColorsCounter.Values.ToList ().FindAll (v => colors.Contains (v.color)).TrueForAll (v => v.Value >= ColorsPerCell); } }

		[SerializeField] RandomStack<Color> randStack = null;
		public Color RandomColor
		{
			get
			{
				if (colors == null || colors.Count == 0) return Random.ColorHSV ();
				if (colors.Count == 1) return colors[0];
				if (randStack == null) randStack = new RandomStack<Color> (colors);
				return randStack.Get ();
			}
		}

		public void OnColorTaked (UnityObjectVariable unityObject)
		{
			ColorVariable colorVar = null;
			if (!unityObject.CheckAs (out colorVar)) return;
			Color color = colorVar.Value;

			ColorCountVariable colorCountVariable;
			ColorsCounter.TryGetValue (color, out colorCountVariable);
			if (colorCountVariable && colorCountVariable.Value < ColorsPerCell) colorCountVariable.Increment ();

		}
		//TODO init Default Colors From Layers from tracks
		// private void InitColors ()
		// {
		// 	var Values = System.Enum.GetValues (typeof (LayerType));
		// 	if (DefaultLayersColors == null || DefaultLayersColors.Count == 0 || DefaultLayersColors.Count != Values.Length)
		// 	{
		// 		DefaultLayersColors = new Dictionary<LayerType, Color> ();
		// 		foreach (LayerType lt in Values)
		// 		{
		// 			DefaultLayersColors.Add (lt, RandomColor);
		// 		}
		// 	}
		// }
		// public Dictionary<LayerType, Color> DefaultLayersColors;
	}
}
