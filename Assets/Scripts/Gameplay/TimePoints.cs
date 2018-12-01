namespace CyberBeat
{
	using FluffyUnderware.Curvy;

	using System;

	using UnityEngine;

	[Serializable]
	public class TimePoints
	{

		public TimePointInfo Start;
		public TimePointInfo End;
	}

	[Serializable]
	public class TimePointInfo
	{
		public Vector3 position;
		public Vector3 Up;
		public Quaternion rotation;
		public float F;
		[Multiline]
		public string MetaData;

		public TimePointInfo (float f, Vector3 _position, Quaternion _rotation, Vector3 up, string metadata = "")
		{

			F = f;
			position = _position;
			rotation = _rotation;
			Up = up;
			MetaData = metadata;
		}

	}
}
