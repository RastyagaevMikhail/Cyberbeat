using DG.Tweening;

using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
	[CreateAssetMenu (fileName = "CenterInputController", menuName = "CyberBeat/InputController/Jump")]
	public class JumpInputController : AInputController
	{
		[SerializeField] UnityEvent jumpStarted;
		[SerializeField] UnityEvent targetInUp;
		[SerializeField] UnityEvent jumpCompleted;
		JumpInputSettings mySettings => (JumpInputSettings) settings;
		Ease easeJumpUp => mySettings.easeJumpUp;
		float jumpUpTime => mySettings.jumpUpTime;
		float jumpHeight => mySettings.jumpHeight;
		float jumpDownTime => mySettings.jumpDownTime;
		Ease easeJumpDown => mySettings.easeJumpDown;
		float jumpHoldTime => mySettings.jumpHoldTime;
		
		public override void TapRight () => Jump ();
		public override void TapLeft () => Jump ();
		public void Jump ()
		{
			if (!Target || (Target && DOTween.IsTweening (Target))) return;

			jumpStarted.Invoke ();

			var TweenUp = Target.DOLocalMoveY (jumpHeight, jumpUpTime).SetEase (easeJumpUp).OnComplete (targetInUp.Invoke);
			var TweenDown = Target.DOLocalMoveY (0, jumpDownTime).SetEase (easeJumpDown)
				.SetDelay (jumpUpTime + jumpHoldTime)
				.OnComplete (jumpCompleted.Invoke);
		}
	}
}
