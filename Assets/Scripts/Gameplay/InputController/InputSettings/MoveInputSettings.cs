using DG.Tweening;

using UnityEngine;

namespace CyberBeat
{
	[CreateAssetMenu(fileName = "MoveInputSettings",menuName ="CyberBeat/InputController/InputSettings/MoveInputSettings")]
	public class MoveInputSettings : InputSettings
	{
		public DG.Tweening.Ease easeMove = Ease.OutQuint;
		public float SwipeDuration = 0.2f;
		public float width = 2.35f;

	}
}
