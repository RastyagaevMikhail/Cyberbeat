using Sirenix.OdinInspector;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class MaterialSwitcher : MonoBehaviour
	{
		public Material CurrentMaterial { get { return renderer.sharedMaterial; } }

		private ParticleSystemRenderer _partRenderer = null;
		public ParticleSystemRenderer partRenderer { get { if (_partRenderer == null) _partRenderer = GetComponent<ParticleSystemRenderer> (); return _partRenderer; } }
		public Color CurrentColor { get { return this [DefaultColorName]; } }
		public Color this [string colorName]
		{
			get { return CurrentMaterial.GetColor (colorName); } set { CurrentMaterial.SetColor (colorName, value); }
		}
		private Renderer _renderer = null;
		public new Renderer renderer { get { if (_renderer == null) _renderer = GetComponent<Renderer> (); return _renderer; } }
		public bool Constant = true;

		[ValueDropdown ("ColorNames")]
		[SerializeField] string DefaultColorName = "_Color";
		static string[] ColorNames = new string[] { "_Color", "_EmissionColor", "_Emis", "_TintColor" };

		public void SetColor (Color newColor)
		{
			SetColorInMaterial (newColor);
		}
		public void SetColorInMaterial (Color newColor)
		{
			// CurrentMaterial.color = newColor;
			this [DefaultColorName] = newColor;
		}
		public void SetColorInMaterial (Color newColor, string colorName = "_Color")
		{
			CurrentMaterial.SetColor (colorName, newColor);
		}
		public void SetMaterial (Material newMaterial)
		{
			renderer.sharedMaterial = newMaterial;
		}
		public bool ChechColor (Color otherColor)
		{
			return this [DefaultColorName].Equals (otherColor);
		}
		public bool ChechColors (Material otherMaterial, string myNameColor = "_Color", string otherNameColor = "_Color")
		{
			return renderer.sharedMaterial.GetColor (myNameColor).Equals (otherMaterial.GetColor (otherNameColor));
		}
		public bool ChechMaterial (Material otherMaterial)
		{
			return CurrentMaterial.Equals (otherMaterial);
		}
		public void SetRandomColor ()
		{
			SetColor (Colors.instance.RandomColor);
		}
	}
}
