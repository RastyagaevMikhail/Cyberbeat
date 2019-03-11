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
		Vector3[] jumpPathUp;
		Vector3[] jumpPathDown;
		public override void Awake ()
		{
			jumpPathUp = new Vector3[] { Vector3.zero, Vector3.up * mySettings.jumpHeight };
			jumpPathDown = new Vector3[] { Vector3.up * mySettings.jumpHeight, Vector3.zero };
		}
		public override void TapRight () => Jump ();
		public override void TapLeft () => Jump ();
		public void Jump ()
		{
			if (!Target || (Target && DOTween.IsTweening (Target))) return;
			jumpStarted.Invoke ();
			var TweenUp = Target.DOLocalPath (jumpPathUp, mySettings.jumpUpTime).SetEase (mySettings.easeJumpUp).OnComplete (targetInUp.Invoke);
			var TweenDown = Target.DOLocalPath (jumpPathDown, mySettings.jumpDownTime).SetEase (mySettings.easeJumpDown)
				.SetDelay (TweenUp.Duration () + mySettings.jumpHoldTime)
				.OnComplete (jumpCompleted.Invoke);
		}
	}
}
