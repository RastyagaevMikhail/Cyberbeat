using DG.Tweening;

using GameCore;


using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CyberBeat
{
	public class MaterialColorSetter : MonoBehaviour
	{

		[SerializeField] Material material;
		[SerializeField] string colorName = "_EmissionColor";
		[SerializeField] float duration = 1f;

		public void GEColor_ChnageColorTo (Color color)
		{
			material.DOColor (color, colorName, duration);
		}
	}
}
