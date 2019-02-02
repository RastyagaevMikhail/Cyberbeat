using GameCore;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Object = UnityEngine.Object;

namespace CyberBeat
{
	[System.Serializable]
	public class RandomConstantMaterial
	{
		public Dictionary<Material, Material> Constant = new Dictionary<Material, Material> ();
		public Dictionary<Material, RandomStack<Material>> RandomSet;
		[SerializeField] Color currentConstatntColor;

		Materials materialsData { get { return Materials.instance; } }
		Color[] colors;
		RandomStack<Color> randColors;

		Material[] BaseMaterials { get { return materialsData.BaseMaterials; } }
		string DefalutColorName { get { return materialsData.DefalutColorName; } }

		public RandomConstantMaterial (Color[] colors)
		{
			this.colors = colors;
			randColors = new RandomStack<Color>(colors);
		}
		public Dictionary<Material, Dictionary<Color, Material>> _materials = null;
		public Dictionary<Material, Dictionary<Color, Material>> materials { get { if (_materials == null) InitDict (); return _materials; } }

		private void InitDict ()
		{
			_materials = BaseMaterials
				.ToDictionary (
					m => m,
					m => colors?
					.ToDictionary (
						c => c,
						c =>
						{
							Material material = Object.Instantiate (m);
							material.SetColor (DefalutColorName, c);
							return material;
						}
					)
				);
		}
		public void SetLastRandomColor (Color lastRandomColor)
		{
			currentConstatntColor = randColors.Get ();

			//Generate Random
			if (colors.Length > 1)
				while (currentConstatntColor == lastRandomColor)
					currentConstatntColor = randColors.Get ();

			RandomSet = new Dictionary<Material, RandomStack<Material>> ();
			foreach (var baseMat in BaseMaterials)
			{
				Constant[baseMat] = materials[baseMat][currentConstatntColor];

				List<Material> ColoredMaterials = new List<Material> (materials[baseMat].Values);

				if (ColoredMaterials.Count > 1 && ColoredMaterials.Contains (Constant[baseMat]))
					ColoredMaterials.Remove (Constant[baseMat]);

				RandomSet[baseMat] = new RandomStack<Material> (ColoredMaterials);
			}
		}

	}
}
