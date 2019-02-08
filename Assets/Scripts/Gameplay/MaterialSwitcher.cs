using DG.Tweening;

using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class MaterialSwitcher : MonoBehaviour
	{
		public Material CurrentMaterial { get { return renderer.sharedMaterial; } set { renderer.sharedMaterial = value; } }

		public Color CurrentColor { get { return this [DefaultColorName]; } set { this [DefaultColorName] = value; } }
		public Color this [string colorName]
		{
			get { return CurrentMaterial.GetColor (colorName); } set { CurrentMaterial.SetColor (colorName, value); }
		}
		private Renderer _renderer = null;
		public new Renderer renderer { get { if (_renderer == null) _renderer = GetComponent<Renderer> (); return _renderer; } }
		public bool Constant = true;
		public bool newMaterialOnAwake;
		[SerializeField] StringVariable DefaultColorNameVariable;
		const string defaultColorName = "_Color";
		public string DefaultColorName { get { return DefaultColorNameVariable?DefaultColorNameVariable.Value : defaultColorName; } }
		private void Awake ()
		{
			if (newMaterialOnAwake)
			{	
				CurrentMaterial = Instantiate(CurrentMaterial);
			}
		}
		public void SetMyColorTo (MaterialSwitcher materialSwitcher)
		{
			materialSwitcher.SetColor (CurrentColor);
		}
		public void SetColor (Color newColor)
		{
			CurrentColor = newColor;
		}

		public void SetMaterial (Material newMaterial)
		{
			renderer.sharedMaterial = newMaterial;
		}
		public bool ChechColor (Color otherColor)
		{
			return CurrentColor.Equals (otherColor);
		}
	}
}
