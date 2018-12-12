using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
	public class GraphicsChnager : MonoBehaviour
	{

		[SerializeField] List<Graphic> graphics = new List<Graphic> ();

		public Color color { set { graphics.ForEach (g => g.color = value); } }

		public void SetColor (Color color) { this.color = color; }

		public void  _SetColorState (int i)
		{
			color = colors[i];
		}

		[SerializeField] List<Color> colors = new List<Color> ();
	}
}
