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
#endif
		[SerializeField] StringVariable defalutColorName;
		public string DefalutColorName { get { return defalutColorName.Value; } }
		private void OnEnable ()
		{
			InitDict ();
		}

		public Dictionary<Material, Dictionary<Color, Material>> _materials = null;
		public Dictionary<Material, Dictionary<Color, Material>> materials { get { if (_materials == null) InitDict (); return _materials; } }
		public Material[] BaseMaterials;

		private void InitDict ()
		{
			_materials = BaseMaterials
				.ToDictionary (
					m => m,
					m => Colors.instance.colors
					.ToDictionary (
						c => c,
						c =>
						{
							Material material = Instantiate (m);
							material.SetColor (DefalutColorName, c);
							return material;
						}
					)
				);
		}
	}
}
