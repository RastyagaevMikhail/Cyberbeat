using GameCore;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
	[System.Serializable]
	public class RandomConstantMaterial
	{
		[SerializeField]
		Dictionary<Material, RandomStack<Material>> randStacks = new Dictionary<Material, RandomStack<Material>> ();
		public Dictionary<Material, Material> Constant = new Dictionary<Material, Material> ();
		[SerializeField] Color currentConstatntColor;
		[SerializeField] Dictionary<Material, RandomStack<Material>> RandomSet;
		Materials materialsData { get { return Materials.instance; } }
		public Colors colorsData { get { return Colors.instance; } }
		string DefalutColorName;
		public void Init (Color lastRandomColor, string ColorName)
		{
			DefalutColorName = ColorName;
			var BaseMaterials = materialsData.BaseMaterials;
			currentConstatntColor = colorsData.RandomColor;

			//Generate Random
			if (colorsData.colors.Count > 1)
				while (currentConstatntColor == lastRandomColor)
					currentConstatntColor = colorsData.RandomColor;

			// Debug.LogFormat ("currentConstatntColor == lastRandomColor  \n{0} == {1}", currentConstatntColor.ToString (false), lastRandomColor.ToString (false));
			materialsData.Init (DefalutColorName);
			foreach (var baseMat in BaseMaterials)
			{

				Constant[baseMat] = materialsData.GetMaterialWhithColor (baseMat, currentConstatntColor, DefalutColorName);
				// Debug.LogFormat ("Constant[mat] = {0}", Constant[mat]);
				RandomSet = new Dictionary<Material, RandomStack<Material>> ();

				List<Material> ColoredMaterials = new List<Material> (materialsData.materials[baseMat]);

				if (ColoredMaterials.Count > 1 && ColoredMaterials.Contains (Constant[baseMat]))
					ColoredMaterials.Remove (Constant[baseMat]);

				RandomSet[baseMat] = new RandomStack<Material> (ColoredMaterials);

				if (randStacks == null) randStacks = new Dictionary<Material, RandomStack<Material>> ();
				randStacks.Add (baseMat, RandomSet[baseMat]);
			}
		}

		public Material GetRandom (Material mat) { return randStacks[mat].Get (); }
	}
}
