using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	[CreateAssetMenu(fileName = "CenterInputController", menuName = "CyberBeat/InputController/Center")]
	public class CenterInputController : AInputController
	{

		Quaternion up { get { return Quaternion.LookRotation (Target.forward, Target.up); } }
		Quaternion left { get { return Quaternion.LookRotation (Target.forward, -Target.right); } }
		Quaternion right { get { return Quaternion.LookRotation (Target.forward, Target.right); } }
		float duration { get { return settings.SwipeDuration / 2f; } }

		Sequence TweenWhithRotateOnUpdate (Quaternion from, Quaternion to, float EndValue = 0)
		{
			Sequence seq = DOTween.Sequence ();
			Tweener moveTween = Target.DOLocalMoveX (EndValue, duration).SetEase (settings.easeMove);
			seq.Append (moveTween);
			seq.Play ();
			return seq;
		}
		//Call In Update
		public override void MoveRight ()
		{
			TweenWhithRotateOnUpdate (up, right, settings.width).OnComplete (() => TweenWhithRotateOnUpdate (up, left));
		}
		public override void MoveLeft ()
		{
			TweenWhithRotateOnUpdate (up, left, -settings.width).OnComplete (() => TweenWhithRotateOnUpdate (up, right));
		}

	}
}
