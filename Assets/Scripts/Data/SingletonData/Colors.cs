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
			foreach (var clr in AllColors)
				clr.Count = Tools.ValidateVaraiable<IntVariable> ($"Assets/Resources/Data/Variables/Colors/{clr.Name}.asset");
			this.Save ();
		}
		public override void ResetDefault ()
		{
		}
#endif
		//---Colors---
		public List<ColorInfo> AllColors;

		private Dictionary<Color, ColorInfo> infoByColor = null;
		private Dictionary<Color, ColorInfo> InfoByColor { get { return infoByColor??(infoByColor = AllColors.ToDictionary (ci => ci.color)); } }
		private Dictionary<string, ColorInfo> infoByName = null;
		private Dictionary<string, ColorInfo> InfoByName { get { return infoByName??(infoByName = AllColors.ToDictionary (ci => ci.Name)); } }

		public ColorInfo this [Color color]
		{
			get
			{
				ColorInfo result = null;
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
