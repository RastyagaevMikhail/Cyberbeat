using GameCore;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
	[System.Serializable]
	public class RandomConstantMaterial
	{
		public Dictionary<Material, Material> Constant = new Dictionary<Material, Material> ();
		public Dictionary<Material, RandomStack<Material>> RandomSet;
		[SerializeField] Color currentConstatntColor;
		Materials materialsData { get { return Materials.instance; } }
		public Colors colorsData { get { return Colors.instance; } }
		Material[] BaseMaterials { get { return materialsData.BaseMaterials; } }

		public RandomConstantMaterial (Color lastRandomColor)
		{
			currentConstatntColor = colorsData.RandomColor;

			//Generate Random
			if (colorsData.colors.Count > 1)
				while (currentConstatntColor == lastRandomColor)
					currentConstatntColor = colorsData.RandomColor;

			RandomSet = new Dictionary<Material, RandomStack<Material>> ();
			foreach (var baseMat in BaseMaterials)
			{
				Constant[baseMat] = materialsData.materials[baseMat][currentConstatntColor];

				List<Material> ColoredMaterials = new List<Material> (materialsData.materials[baseMat].Values);

				if (ColoredMaterials.Count > 1 && ColoredMaterials.Contains (Constant[baseMat]))
					ColoredMaterials.Remove (Constant[baseMat]);

				RandomSet[baseMat] = new RandomStack<Material> (ColoredMaterials);
			}
		}
	}
}
