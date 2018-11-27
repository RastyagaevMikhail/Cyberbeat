using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameCore;
namespace CyberBeat
{
	using Sirenix.OdinInspector;

	using UnityEngine;

	[CreateAssetMenu (fileName = "ColorCount", menuName = "Variables/CyberBeat/ColorCount", order = 0)]
	public class ColorCountVariable : IntVariable
	{
		public Color color;

#if UNITY_EDITOR
		[Button]
		void Clear ()
		{
			Value = 0;
			SaveValue ();
		}
#endif
	}
}
