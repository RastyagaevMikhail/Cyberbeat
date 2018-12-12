namespace CyberBeat
{
	using GameCore;

	using Sirenix.OdinInspector;

	using System;

	using UnityEngine;
	[ExecuteInEditMode]
	public class MetaDataOptions : SingletonData<MetaDataOptions>
	{
#if UNITY_EDITOR
		[UnityEditor.MenuItem ("Game/Data/MetaDataOptions")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
		public override void ResetDefault () { }
		public override void InitOnCreate () { }
#endif

		[Toggle ("enabled")]
		public SpeedMetaDataOption _speedMetaData = new SpeedMetaDataOption ();
		public static SpeedMetaDataOption speedMetaData { get { return instance._speedMetaData; } }

		[Toggle ("enabled")]
		[SerializeField] BitViewerOptions _bitViewer;

		public static BitViewerOptions bitViewer { get { return instance._bitViewer; } }

		[Serializable]
		public class BitViewerOptions : MetaDataOption
		{
			[Range (0, 1)]
			public float SizePoint = 0.2f;
			[Range (0, 10)]
			public float OffsetSizePoint = 5;
			public Color PointsColor = Color.magenta;
		}

		[Serializable]
		public class SpeedMetaDataOption : MetaDataOption
		{
			public bool ShowPoints = true;
			[ShowIf ("ShowPoints")]
			[Range (1, 100)]
			public int CountPoints = 10;
			[ShowIf ("ShowPoints")]
			public bool ShowPossition = false;
			public Color GradienSpeedColorOne = Color.red;
			public Color GradienSpeedColorTwo = Color.blue;
			public int OutlineSize = 2;
		}

		[Serializable]
		public class MetaDataOption
		{
			public bool enabled;
			/* 	[HideInInspector]
				public string Title;
				public string someSthing;
				string Name { get { return GetType ().Name; } } */
		}
	}
}
