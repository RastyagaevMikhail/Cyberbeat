using SonicBloom.Koreo;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
#endif
namespace CuberBeat
{

	public abstract class PayloadData : ScriptableObject, IPayload
	{
		public bool DoGUI (Rect displayRect, KoreographyTrack track, bool isSelected)
		{
#if UNITY_EDITOR
		
#endif
			return isSelected;
		}

		public abstract IPayload GetCopy ();

	}
}
