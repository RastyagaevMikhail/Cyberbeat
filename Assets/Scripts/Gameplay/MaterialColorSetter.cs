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
		public void GEColor_ChnageColorTo (Color color)
		{
			foreach (var set in settings)
				set.SetColor (color);
		}
	}

	[System.Serializable]

	public class MaterialColorSetterSettings
	{
		[HideInInspector]
		[SerializeField] string Name;
		[SerializeField] Material material;
		[SerializeField] StringVariable ColorName;
		[SerializeField] float duration = 1f;
		public void SetColor (Color color)
		{
			material.DOColor (color, ColorName ? ColorName.Value : "_Color", duration);
		}

		public void OnValidate ()
		{
			string materialName = material ? material.name : "";
			string colorName = ColorName ? ColorName.Value : "_Color";
			Name = $"{materialName}.{colorName}.{duration}sec";
		}
	}
}
