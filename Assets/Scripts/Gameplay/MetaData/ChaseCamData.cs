namespace CyberBeat
{
	using EZCameraShake;

	using UnityEngine;
	[System.Serializable]
	public struct ChaseCamData : GameCore.IMetaData
	{
		[Header ("Позиция камеры")]
		public Vector3 TargetMovePosition;
		[Header ("Точка куда смотрит камера")]
		public Vector3 TargetLookPosition;
		[Header ("Время на движение камеры")]
		public float TimeChase;
		[Header ("Время для достижения камеры позиции TargetMovePosition")]
		public float DurationTimeOfMove;
		[Header ("Время для достижения камеры позиции TargetLookPosition")]
		public float DurationTimeOfLook;

		float timeDuaration;
		public float TimeDuaration { get; set; }

		public override string ToString ()
		{
			return 	$"TargetMovePosition:{TargetMovePosition}\n"+
					$"TargetLookPosition:{TargetLookPosition}\n"+
					$"TimeChase:{TimeChase}\n"+
					$"DurationTimeOfMove:{DurationTimeOfMove}\n"+
					$"DurationTimeOfLook:{DurationTimeOfLook}";
		}

	}
}
