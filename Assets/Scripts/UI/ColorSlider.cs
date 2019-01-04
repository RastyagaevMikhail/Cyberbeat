using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
namespace CyberBeat
{
	public class ColorSlider : MonoBehaviour
	{
		[SerializeField] Image Fill;
		[SerializeField] Image Frame;
		[SerializeField] IntVariable countVariable;
		public Color color { get { return Fill.color; } set { Fill.color = value; Frame.color = value; } }
		public float progress { get { Debug.LogFormat ("this = {0}", this); Debug.LogFormat ("Fill = {0}", Fill); return Fill.fillAmount; } set { Fill.fillAmount = value; } }
		public Colors colorsData { get { return Colors.instance; } }
		float ColorsPerCell { get { return colorsData.ColorsPerCell; } }


		public void Init (ColorInfo colorInfo)
		{
			countVariable = colorInfo.Count;
			this.color = colorInfo.color;
			countVariable.OnValueChanged += OnChangedPorgress;
			OnChangedPorgress (countVariable.Value);
		}
		private void OnDisable ()
		{
			if (countVariable)
				countVariable.OnValueChanged -= OnChangedPorgress;
		}
		private void OnChangedPorgress (int count)
		{
			float value = (float) count / ColorsPerCell;
			this.progress = value;
		}

	}
}
