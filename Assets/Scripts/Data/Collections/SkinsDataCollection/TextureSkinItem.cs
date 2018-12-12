using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class TextureSkinItem : SkinItem
	{
		Texture texture { get { return Prefab as Texture; } }
		public override void Apply (Object target, params object[] args)
		{
			// Debug.Log ("Apply Road Skin");
			Material mat = null;
			bool isApplied = Applyed (out mat, target);
			if (!isApplied) return;

			bool isValidArgs = args != null &&
				args.Length == 1 &&
				args[0] as string != null;

			// Debug.LogFormat ("texture = {0}", texture);
			// if (isValidArgs)
			mat.SetTexture ("_EmissionMap", texture);
			// else
			// mat.mainTexture = texture;
		}
	}
}
