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
		[SerializeField] string Name;
		[SerializeField] Material material;
		[SerializeField] StringVariable ColorName;
		string colorName => ColorName ? ColorName.Value : "_Color";
		[SerializeField] float duration = 1f;
		public void SetColor (Color color)
		{
			// material.DOColor (color, colorName, duration);
			material.SetColor (colorName, color); 
		}

		public void OnValidate ()
		{
			string materialName = material ? material.name : "";
			string colorName = ColorName ? ColorName.Value : "_Color";
			Name = $"{materialName}.{colorName}.{duration}sec";
		}
	}
}
