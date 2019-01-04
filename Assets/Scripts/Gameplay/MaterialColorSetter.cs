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
		[SerializeField] StringVariable ColorName;
		[SerializeField] float duration = 1f;

		public void GEColor_ChnageColorTo (Color color)
		{
			material.DOColor (color, ColorName ? ColorName.Value : "_Color", duration);
		}
	}
}
