using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
	public class RectTransformObject : TransformObject
	{
		private RectTransform _rectTransform = null;
		public RectTransform rectTransform { get { if (_rectTransform == null) _rectTransform = GetComponent<RectTransform> (); return _rectTransform; } }
		public Vector3 anchoredPosition { get { return rectTransform.anchoredPosition3D; } set { rectTransform.anchoredPosition3D = value; } }
		public override float x { get { return anchoredPosition.x; } set { anchoredPosition = new Vector3 (value, y, z); } }
		public override float y { get { return anchoredPosition.y; } set { anchoredPosition = new Vector3 (x, value, z); } }
		public override float z { get { return anchoredPosition.z; } set { anchoredPosition = new Vector3 (x, y, value); } }
		public Vector2 sizeDelta { get { return rectTransform.sizeDelta; } set { rectTransform.sizeDelta = value; } }
	}
}
