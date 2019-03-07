using DG.Tweening;

using UnityEngine;

namespace CyberBeat
{
	[CreateAssetMenu (fileName = "InputSettings", menuName = "CyberBeat/InputSettings", order = 0)]
	public class InputSettings : ScriptableObject
	{

		[Header ("Move Settings")]
		public DG.Tweening.Ease easeMove = Ease.OutQuint;
		public float SwipeDuration = 0.2f;
		public float width = 2.35f;
		[Header ("Jump Settings")]
		public float jumpHeight = 2.5f;
		[Header ("Up")]
		public float jumpUpTime = 1f;
		public DG.Tweening.Ease easeJumpUp = Ease.OutQuint;
		[Header ("Hold")]
		public float jumpHoldTime = 0f;
		[Header ("Down")]
		public float jumpDownTime = 1f;
		public DG.Tweening.Ease easeJumpDown = Ease.InExpo;

	}
}
