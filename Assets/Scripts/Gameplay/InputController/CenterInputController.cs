using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class CenterInputController : IInputController
	{

		Quaternion up { get { return Quaternion.LookRotation (Target.forward, Target.up); } }
		Quaternion left { get { return Quaternion.LookRotation (Target.forward, -Target.right); } }
		Quaternion right { get { return Quaternion.LookRotation (Target.forward, Target.right); } }
		float duration { get { return settings.SwipeDuration / 2f; } }
		Transform Target;
		InputSettings settings;
		public void Init (Transform target, InputSettings _settings)
		{
			Target = target;
			settings = _settings;
		}
		Sequence TweenWhithRotateOnUpdate (Quaternion from, Quaternion to, float EndValue = 0)
		{
			Sequence seq = DOTween.Sequence ();
			Tweener moveTween = Target.DOLocalMoveX (EndValue, duration).SetEase (settings.easeMove);
			seq.Append (moveTween);
			seq.Play ();
			return seq;
		}
		//Call In Update
		public void MoveRight ()
		{
			TweenWhithRotateOnUpdate (up, right, settings.width).OnComplete (() => TweenWhithRotateOnUpdate (up, left));
		}
		public void MoveLeft ()
		{
			TweenWhithRotateOnUpdate (up, left, -settings.width).OnComplete (() => TweenWhithRotateOnUpdate (up, right));
		}

	}
}
