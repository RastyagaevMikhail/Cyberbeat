using GameCore;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Random = UnityEngine.Random;

namespace CyberBeat
{

	public class Colors : SingletonData<Colors>
	{
#if UNITY_EDITOR
		[UnityEditor.MenuItem ("Game/Data/Colors")] public static void Select () { UnityEditor.Selection.activeObject = instance; }

		public override void InitOnCreate () { GenerateColorsCount (); }

		[ContextMenu ("Generate Colors Count")]
		void GenerateColorsCount ()
		{
			colorsCounter = new ColorsCounter ();
			foreach (var clr in AllColors)
			{
				var colorCountVar = ScriptableObject.CreateInstance<ColorCountVariable> ();
				colorCountVar.color = clr;
				var nameVariable = "#{0}".AsFormat (clr.ToString (false));
				colorsCounter.counts.Add (new ColorCount () { color = clr, variable = colorCountVar });
				Tools.CreateAsset (colorCountVar, "Assets/Resources/Data/Variables/Colors/{0}.asset".AsFormat (nameVariable));
			}
			this.Save ();
		}
		public override void ResetDefault ()
		{
			levelOnColorProgress.Value = 0;
		}
#endif
		//---Colors---
		private List<Color> _colors = null;
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

		public ColorsCounter colorsCounter = new ColorsCounter ();
		public bool ColorsCounterIsFull { get { return colorsCounter.Variables.ToList ().FindAll (v => colors.Contains (v.color)).TrueForAll (v => v.Value >= ColorsPerCell); } }

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

			ColorCountVariable colorCountVariable = colorsCounter[color];
			if (colorCountVariable && colorCountVariable.Value < ColorsPerCell) colorCountVariable.Increment ();

		}

		public void UpdateRandomColorsStack ()
		{
			randStack = new RandomStack<Color> (colors);
		}

		[ContextMenu ("Init Colors")]
		private void InitColors ()
		{
			var Values = System.Enum.GetValues (typeof (LayerType));
			if (DefaultLayersColors == null || DefaultLayersColors.Count == 0 || DefaultLayersColors.Count != Values.Length)
			{
				DefaultLayersColors = new LayersColors ();
				foreach (LayerType lt in Values)
				{
					DefaultLayersColors.layerOfColors.Add (new LayerOfColor () { layer = lt, color = RandomColor });
				}
			}
		}
		public LayersColors DefaultLayersColors;
	}

	[Serializable]
	public class LayersColors
	{
		public List<LayerOfColor> layerOfColors;
		public int Count { get { return layerOfColors.Count; } }

		public Color this [LayerType layer]
		{
			get
			{
				return layerOfColors.Find (lc => lc.layer == layer).color;
			}
		}
	}

	[Serializable]
	public class LayerOfColor
	{
		public LayerType layer;
		public Color color;
	}

	[Serializable]
	public class ColorsCounter
	{
		public List<ColorCountVariable> Variables
		{
			get
			{
				return counts.Select (c => c.variable).ToList ();
			}
		}

		public List<ColorCount> counts = new List<ColorCount> ();

		public ColorCountVariable this [Color color]
		{
			get
			{
				return counts.Find (counts => counts.color == color).variable;
			}
		}
	}

	[Serializable]
	public class ColorCount
	{
		public Color color;
		public ColorCountVariable variable;
	}
}
