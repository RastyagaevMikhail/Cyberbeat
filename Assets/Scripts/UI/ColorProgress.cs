using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;
namespace CyberBeat
{
	public class ColorProgress : MonoBehaviour
	{
		// private RectTransform _rectTransform = null;
		// public RectTransform rectTransform { get { if (_rectTransform == null) _rectTransform = GetComponent<RectTransform> (); return _rectTransform; } }
		private GameEventListeners _listeners = null;
		public GameEventListeners listeners { get { if (_listeners == null) _listeners = GetComponent<GameEventListeners> (); return _listeners; } }

		[SerializeField] ColorSliders colors = new ColorSliders ();
		[SerializeField] ColorSlider ColorPrefab;
		[SerializeField] GameObject Empty;
		Colors data { get { return Colors.instance; } }

		void Awake ()
		{
			_UpdateColors (data.LevelOnColorProgress);
		}

		public void _UpdateColors (int counrcolors)
		{
			Empty.transform.SetParent (transform.parent);
			transform.DestroyAllChilds ();
			colors = new ColorSliders ();
			// var width = rectTransform.sizeDelta.x;
			foreach (var color in data.colors)
			{
				ColorCountVariable variable = data.colorsCounter[color];
				var clr = Instantiate (ColorPrefab, transform);
				clr.name = string.Format ("{0}", variable.name);
				colors.sliders.Add (new SliderOfColor () { color = color, slider = clr });
				clr.Init (variable);
			}
			Empty.transform.SetParent (transform);
			bool emptyIsEnabled = data.colors.Count <= 6;
			Empty.SetActive (emptyIsEnabled);
			/* if (emptyIsEnabled)
			{
				Empty.transform.SetAsLastSibling ();
			} */
		}

	}

	[Serializable]
	public class ColorSliders
	{
		public List<SliderOfColor> sliders = new List<SliderOfColor>();
		public ColorSlider this [Color color]
		{
			get
			{
				return sliders.Find (sl => sl.color == color).slider;
			}
		}
	}

	[Serializable]
	public class SliderOfColor
	{
		public Color color;
		public ColorSlider slider;
	}
}
