using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	using UnityEngine;

	[CreateAssetMenu (fileName = "ColorCount", menuName = "Variables/CyberBeat/ColorCount", order = 0)]
	public class ColorCountVariable : IntVariable
	{
		public Color color;

#if UNITY_EDITOR
		[ContextMenu ("Clear")]
		void Clear ()
		{
			Value = 0;
			SaveValue ();
		}
#endif
	}
}
