﻿using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
	public class SkinTypeViewSettingsDataSelector : AEnumDataSelectorMonoBehaviour<SkinType, ViewSettings>
	{
		[SerializeField] List<ViewSettingsSkinTypeData> datas;
		public override List<TypeData<SkinType, ViewSettings>> Datas
		{
			get
			{
				return datas.Cast<TypeData<SkinType, ViewSettings>> ().ToList ();
			}
		}
	}

	[Serializable]
	public class ViewSettingsSkinTypeData : TypeData<SkinType, ViewSettings> { }

	[Serializable]
	public class ViewSettings
	{
		public Transform LookTarget;
		public Transform PositionTarget;
		public float RotationAngle;
		public float FogDensity;
		public float DurationMove = 1f;

	}
}
