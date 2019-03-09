using DG.Tweening;

using UnityEngine;

namespace CyberBeat
{
	[CreateAssetMenu (fileName = "JumpInputSettings", menuName = "CyberBeat/InputController/InputSettings/JumpInputSettings")]
	public class JumpInputSettings : InputSettings
	{
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
