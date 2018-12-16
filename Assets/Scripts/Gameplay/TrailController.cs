using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class TrailController : MonoBehaviour
	{
		private TrailRenderer _renderer = null;
		public new TrailRenderer renderer { get { if (_renderer == null) _renderer = GetComponent<TrailRenderer> (); return _renderer; } }

		public void SetColor (Color color)
		{
			Debug.Log ("TrailController.SetColor");
			Debug.LogFormat ("color = {0}", color);
			renderer.colorGradient.SetKeys (new GradientColorKey[]
				{
					new GradientColorKey (color, 0),
						new GradientColorKey (Color.white, 1)
				},
				new GradientAlphaKey[]
				{
					new GradientAlphaKey (1, 0),
						new GradientAlphaKey (1, 1),
				});
		}
	}
}
