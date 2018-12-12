using GameCore;

using SonicBloom.Koreo;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{

	public class KoreoTools
	{
		public static void OpenKoreographyEditor (Koreography koreography, KoreographyTrack koreographyTrack)
		{
			var methodInfo = Tools.EditorAssembliesHelper.GetMethodInCalss ("CyberBeat.KoreoEditorTools", "OpenEditorByDifficulty");

			methodInfo.Invoke (null, new object[] { koreography, koreographyTrack });
		}
		
	}
}
