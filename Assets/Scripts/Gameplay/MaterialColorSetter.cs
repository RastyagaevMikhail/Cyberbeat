using DG.Tweening;

using GameCore;

using Sirenix.OdinInspector;
using Sirenix.Serialization;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CyberBeat
{
	public class MaterialColorSetter : MonoBehaviour
	{
		[ShowIf ("materialNotNull")]
		[SerializeField] ShaderProperties Props = null;
		private void OnValidate ()
		{
			if (material)
			{
				if (Props == null) Props = new ShaderProperties (material.shader, 1); // get Colros
				Props.OnValidate ();
			}
		}
		bool materialNotNull { get { return material; } }

		[SerializeField] Material material;

		// [SerializeField] string ColorName = "_EmissionColor";
		[SerializeField] float duration = 1f;

		public void ChnageColorTo (UnityObjectVariable unityObjectVariable)
		{
			ColorVariable colorVariable;
			if (!unityObjectVariable.CheckAs<ColorVariable> (out colorVariable)) return;

			Color color = colorVariable.Value;
			material.DOColor (color, Props, duration);
		}
	}
}
