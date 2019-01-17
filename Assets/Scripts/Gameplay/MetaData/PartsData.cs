using UnityEngine;

namespace CyberBeat
{
	[System.Serializable]
	public class PartsData : GameCore.IMetaData
	{
		public bool MoveOnBit;
		public float StartLifetime;
		public float StartSpeed;
		public float StartSize;
		public float LengthScale;
		public float OverRate;
		public float timeDuaration;
		public float TimeDuaration { get { return timeDuaration; } set { timeDuaration = value; } }
	}
}
