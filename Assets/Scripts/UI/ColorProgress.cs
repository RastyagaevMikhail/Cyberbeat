using GameCore;

using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;
namespace CyberBeat
{
	public class ColorProgress : SerializedMonoBehaviour
	{
		// private RectTransform _rectTransform = null;
		// public RectTransform rectTransform { get { if (_rectTransform == null) _rectTransform = GetComponent<RectTransform> (); return _rectTransform; } }
		private GameEventListeners _listeners = null;
		public GameEventListeners listeners { get { if (_listeners == null) _listeners = GetComponent<GameEventListeners> (); return _listeners; } }

		[SerializeField] Dictionary<Color, ColorSlider> colors = new Dictionary<Color, ColorSlider> ();
		[SerializeField] ColorSlider ColorPrefab;
		Colors data { get { return Colors.instance; } }
		
		void Awake ()
		{
			colors = new Dictionary<Color, ColorSlider> ();
			// var width = rectTransform.sizeDelta.x;
			foreach (var color in data.colors)
			{
				ColorCountVariable variable = data.ColorsCounter[color];
				var clr = Instantiate (ColorPrefab, transform);
				clr.name = string.Format ("{0}", variable.name);
				colors.Add (color, clr);
				clr.Init (variable);
			}
		}
		
	}
}
