﻿using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class TextureSkinItem : SkinItem
	{
		public Texture texture { get { return Prefab as Texture; } }

		[SerializeField] TextureProperty property;

		public override void Apply (Object target, params object[] args)
		{
			property.property = texture;
		}
	}
}
