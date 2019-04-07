using DG.Tweening;

using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CyberBeat
{
	public class MaterialColorSetter : MonoBehaviour
	{
		[SerializeField] List<MaterialColorSetterSettings> settings;
		[SerializeField] int IndexOfRemove;
		[SerializeField] bool debug;
		[ContextMenu("Validate")]
		private void OnValidate ()
		{
			foreach (var set in settings)
				set.OnValidate ();
		}
		private void OnEnable ()
		{
			foreach (var set in settings)
				set.SetColor (Color.white);
		}
		public void GEColor_ChnageColorTo (ColorVariable color)
		{
			GEColor_ChnageColorTo (color.Value);
		}
		public void GEColor_ChnageColorTo (Color color)
		{
			foreach (var set in settings)
				set.SetColor (color);
		}

		[ContextMenu ("RemoveByIndex")]
		void RemoveByIndex ()
		{
			settings.RemoveAt (IndexOfRemove);
		}
	}

	[System.Serializable]

	public class MaterialColorSetterSettings
	{
	
		[HideInInspector]
		[SerializeField] int nameHash = 0;
		[SerializeField] Material material;
		[SerializeField] StringVariable ColorName;
		string colorName => ColorName ? ColorName.Value : "_Color";
		public void SetColor (Color color) => material.SetColor (colorName, color);
		public void OnValidate ()
		{
			if (nameHash == 0)
				nameHash = Shader.PropertyToID (colorName);

		}
	}
}
