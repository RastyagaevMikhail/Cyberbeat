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
		[Header ("Использовать тряску?")]
		public bool useShake;
		[Header ("Интенсивность встряхивания")]
		public float magnitude;
		[Header ("Шероховатость встряхивания. Более низкие значения более плавные, более высокие значения более резкие")]
		public float roughness;
		[Header ("Как долго исчезать в тряске, в секундах")]
		public float fadeInTime;
		[Header ("Как долго исчезать дрожь, через несколько секунд")]
		public float fadeOutTime;
		[Header ("Насколько это встряска влияет на положение.")]
		public Vector3 posInfluence;
		[Header ("Насколько это дрожание влияет на вращение.")]
		public Vector3 rotInfluence;

		float timeDuaration;
		public float TimeDuaration { get;set; }

		// TODO Make EZ ShakeCameara

	}
}
