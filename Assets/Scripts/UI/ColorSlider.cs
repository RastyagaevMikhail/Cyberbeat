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
		[SerializeField] ColorCountVariable countVariable;
		public Color color { get { return Fill.color; } set { Fill.color = value; Frame.color = value; } }
		public float progress { get { Debug.LogFormat ("this = {0}", this); Debug.LogFormat ("Fill = {0}", Fill); return Fill.fillAmount; } set { Fill.fillAmount = value; } }
		public Colors colorsData { get { return Colors.instance; } }
		float ColorsPerCell { get { return colorsData.ColorsPerCell; } }

		[SerializeField] bool InitOnAwake = false;
		private void Awake ()
		{
			if (InitOnAwake && countVariable)
			{
				Init (countVariable);
			}
		}
		public void Init (ColorCountVariable variable)
		{
			countVariable = variable;
			this.color = countVariable.color;
			countVariable.OnValueChanged += OnChangedPorgress;
			OnChangedPorgress (countVariable.Value);
		}
		private void OnDestroy ()
		{
			countVariable.OnValueChanged -= OnChangedPorgress;
		}
		private void OnChangedPorgress (int count)
		{
			float value = (float) count / ColorsPerCell;
			this.progress = value;
		}

	}
}
