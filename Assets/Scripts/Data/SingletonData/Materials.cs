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
		public override void InitOnCreate ()
		{

		}

		public override void ResetDefault () { }
#endif

		Dictionary<Material, List<Material>> _materials = null;
		public Dictionary<Material, List<Material>> materials { get { return _materials; } }
		Dictionary<Material, RandomStack<Material>> randStacks = null;
		public Material[] BaseMaterials;
		bool NotInited
		{
			get
			{
				List<List<Material>> list = _materials != null?_materials.Values.Cast<List<Material>> ().ToList () : null;
				return materials == null || _materials.Count == 0 || list != null && list.TrueForAll (l =>
				{
					bool isNoInited = l.TrueForAll (m => m == null) || l.Count == 0;
					return isNoInited;
				});
			}
		}
		public void Init (string defalutColorName)
		{
			_materials = new Dictionary<Material, List<Material>> ();
			foreach (var mat in BaseMaterials)
				Add (mat, defalutColorName);
		}
		/* public Material GetRandomMaterial (Material mat)
		{
			if (randStacks == null)
			{
				Init ();
				randStacks = new Dictionary<Material, RandomStack<Material>> ();
				foreach (var m in BaseMaterials)
				{
					List<Material> colection = materials[m];
					randStacks[m] = new RandomStack<Material> (colection);
				}
			}
			return randStacks[mat].Get ();
		}
 */
		public Material GetMaterialWhithColor (Material mat, Color color, string ColorName)
		{
			var LogStr = Tools.LogCollection (materials.Keys);
			// Debug.Log (LogStr);
			LogStr = Tools.LogCollection (materials[mat]);
			// Debug.Log (LogStr);
			// Debug.Log (color.ToString (false));
			return materials[mat].Find (m => m.GetColor (ColorName) == (color));
		}
		public void Add (Material BaseMaterial, string ColorName = "_Color")
		{
			foreach (var color in Colors.instance.colors)
			{
				Material mat = UnityEngine.Object.Instantiate (BaseMaterial);

				mat.SetColor (ColorName, color);
				mat.name = string.Format ("{0} #{1}", BaseMaterial.name, color.ToString (false));

				if (!materials.ContainsKey (BaseMaterial))
					materials.Add (BaseMaterial, new List<Material> ());

				List<Material> list = materials[BaseMaterial];
				bool IsContainsNull = list.Any (m => m == null);
				if (IsContainsNull)
				{
					materials[BaseMaterial] = new List<Material> ();
					materials[BaseMaterial].Add (mat);
				}
				bool isExist = materials[BaseMaterial].Exists (m => mat.GetColor (ColorName) == m.GetColor (ColorName));
				if (!isExist)
					materials[BaseMaterial].Add (mat);
			}

		}
	}
}
