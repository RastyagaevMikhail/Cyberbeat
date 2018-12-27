using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
	public class ScrollSettingsDataSelector : TransformGroupSelector<SkinType>
	{
		public override TransformGroup this [SkinType enumType]
		{
			get
			{
			return base[enumType] as TransformGroup;
			}
		}

		[SerializeField] List<ScrollSettingsData> datas;
		public override List<TypeData<SkinType, TransformGroup>> Datas
		{
			get
			{
				return datas.Cast<TypeData<SkinType, TransformGroup>> ().ToList();
			}
		}
	}

	[Serializable]
	public class ScrollSettingsData : TypeData<SkinType, TransformGroup> { }
}
