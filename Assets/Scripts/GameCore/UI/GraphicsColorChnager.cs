using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
	public class GraphicsColorChnager : MonoBehaviour
	{
		[SerializeField] List<Graphic> excludeGraphics = new List<Graphic> ();
		[SerializeField] List<Graphic> graphics = new List<Graphic> ();
		private void OnValidate ()
		{
			graphics = GetComponentsInChildren<Graphic> ().Except (excludeGraphics).ToList ();
		}
		public Color color { set { graphics.ForEach (g => g.color = value); } }
		public void SetColor (ColorVariable variable)
		{
			color = variable.ValueFast;
		}

	}
}
