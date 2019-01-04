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
			// foreach (var clr in AllColors)
			// {
			// 	var colorCountVar = ScriptableObject.CreateInstance<ColorCountVariable> ();
			// 	colorCountVar.color = clr;
			// 	var nameVariable = "#{0}".AsFormat (clr.ToString (false));
			// 	colorsCounter.counts.Add (new ColorCount () { color = clr, variable = colorCountVar });
			// 	Tools.CreateAsset (colorCountVar, "Assets/Resources/Data/Variables/Colors/{0}.asset".AsFormat (nameVariable));
			// }
			// this.Save ();
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
						_colors.Add (AllColors[i].color);
				}
				return _colors;
			}
		}

		private List<ColorInfo> avalivableColors = null;
		public List<ColorInfo> AvalivableColors
		{
			get
			{
				if (avalivableColors == null || avalivableColors.Count == 0)
					avalivableColors = new List<ColorInfo> (AllColors.Take (LevelOnColorProgress + 1));
				return avalivableColors;
			}
		}
		public List<ColorInfo> AllColors;
		int[] colorsByLevel = new int[] { 50, 100, 50, 150, 75, 50, 200 };
		[SerializeField] IntVariable levelOnColorProgress;
		public int LevelOnColorProgress { get { return levelOnColorProgress.Value; } set { levelOnColorProgress.Value = value; } }

		public int ColorsPerCell { get { return colorsByLevel.InRangeIndex (levelOnColorProgress.Value) ? colorsByLevel[levelOnColorProgress.Value] : 50; } }

		public bool ColorsCounterIsFull;
		//  { get { return colorsCounter.Variables.ToList ().FindAll (v => colors.Contains (v.color)).TrueForAll (v => v.Value >= ColorsPerCell); } }

		[SerializeField] RandomStack<Color> randStack = null;
		private Dictionary<Color, ColorInfo> infoByColor = null;
		private Dictionary<Color, ColorInfo> InfoByColor { get { return infoByColor??(infoByColor = AllColors.ToDictionary (ci => ci.color)); } }
		private Dictionary<string, ColorInfo> infoByName = null;
		private Dictionary<string, ColorInfo> InfoByName { get { return infoByName??(infoByName = AllColors.ToDictionary (ci => ci.Name)); } }

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

			// ColorCountVariable colorCountVariable = colorsCounter[color];
			// if (colorCountVariable && colorCountVariable.Value < ColorsPerCell) colorCountVariable.Increment ();

		}

		public void UpdateRandomColorsStack ()
		{
			randStack = new RandomStack<Color> (colors);
		}

		public ColorInfo this [Color color]
		{
			get
			{
				ColorInfo result = null;
				Debug.LogFormat ("color = {0}", color);
				Debug.Log (Tools.LogCollection (InfoByColor.Keys));
				InfoByColor.TryGetValue (color, out result);
				return result;
			}
		}
		public ColorInfo this [string name]
		{
			get
			{
				ColorInfo result = null;
				InfoByName.TryGetValue (name, out result);
				return result;
			}
		}

	}

	[Serializable]
	public class ColorInfo
	{
		public string Name;
		public Color color;
		public IntVariable Count;

	}
}
