using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class TextureSkinItem : SkinItem
	{

		Texture texture { get { return Prefab as Texture; } }

		[HideInInspector]
		[SerializeField] int hashName = 0;
		private void OnValidate ()
		{
			if (hashName == 0)
				hashName = Shader.PropertyToID ("_Main");
		}

		public override void Apply (Object target, params object[] args)
		{
			// Debug.Log ("Apply Road Skin");
			Material mat = null;
			bool isApplied = Applyed (out mat, target);
			if (!isApplied) return;

			bool isValidArgs = args != null &&
				args.Length == 1 &&
				args[0] as string != null;

			mat.SetTexture (hashName, texture);
		}
	}
}
