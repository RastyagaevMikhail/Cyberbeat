using SonicBloom.Koreo;
using SonicBloom.Koreo.EditorUI;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class KoreoEditorTools : MonoBehaviour
	{
		public static void OpenEditorByDifficulty (Koreography koreography, KoreographyTrack koreographyTrack)
		{
			KoreographyEditor.OpenKoreography (koreography, koreographyTrack);
		}
	}
}
