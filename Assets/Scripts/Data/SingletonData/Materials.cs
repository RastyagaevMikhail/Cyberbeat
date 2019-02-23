using GameCore;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
	public class Materials : SingletonData<Materials>
	{
#if UNITY_EDITOR
		[UnityEditor.MenuItem ("Game/Data/Materials")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
		public override void InitOnCreate () { }
		public override void ResetDefault () { }
#else
		public override void ResetDefault () { }
#endif

		[SerializeField] StringVariable defalutColorName;
		public string DefalutColorName { get { return defalutColorName.Value; } }
		public Material[] BaseMaterials;
	}
}
