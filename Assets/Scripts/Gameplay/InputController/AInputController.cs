using DG.Tweening;

using GameCore;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
	public abstract class AInputController : ScriptableObject
	{
		public abstract void Awake ();
		public abstract void MoveRight ();
		public abstract void MoveLeft ();
		protected Vector3[] rightPath;
		protected Vector3[] leftPath;
		protected Vector3[] jumpPathUp => new Vector3[] { Vector3.zero, Vector3.up * settings.jumpHeight };
		protected Vector3[] jumpPathDown => new Vector3[] { Vector3.up * settings.jumpHeight, Vector3.zero };
		[SerializeField] RigidbodyVariable targetVariable;
		protected Rigidbody Target { get { return targetVariable.ValueFast; } }
		protected MonoBehaviour monoTarget = null;
		protected MonoBehaviour MonoTarget => monoTarget ?? (monoTarget = Target.GetComponent<MonoBehaviour> ());
		Transform parent => Target.transform.parent;
		[SerializeField] protected InputSettings settings;
		[SerializeField] UnityEvent jumpCompleted;
		public void Jump ()
		{
			if (!Target ||(Target && DOTween.IsTweening(Target))) return;

			// var TweenUp = DOVirtual.Float (0, settings.jumpHeight, settings.jumpUpTime, MoveLocalY).SetEase (settings.easeJumpUp);
			// var TweenDown = DOVirtual.Float (settings.jumpHeight, 0, settings.jumpDownTime, MoveLocalY).SetEase (settings.easeJumpDown);
			// TweenUp
			// 	.OnComplete (() => TweenDown
			// 		.SetDelay (settings.jumpHoldTime)
			// 		.OnComplete (OnJumpCompleted)
			// 		.Play ())
			// 	.Play ();

			var TweenUp = Target.DOLocalPath (jumpPathUp, settings.jumpUpTime).SetEase (settings.easeJumpUp);
			var TweenDown = Target.DOLocalPath (jumpPathDown, settings.jumpDownTime).SetEase (settings.easeJumpDown)
				.SetDelay (TweenUp.Duration () + settings.jumpHoldTime)
				.OnComplete(jumpCompleted.Invoke);

			// var seq = DOTween.Sequence ()	
			// 	.Insert (0, TweenUp)
			// 	// .AppendInterval (settings.jumpHoldTime)
			// 	// .Insert (TweenUp.Duration (), TweenDown)
			// 	;
			// seq.Play ();

		}

		private void OnJumpCompleted ()
		{
			Target.velocity = Vector3.zero;
			jumpCompleted.Invoke ();
		}

		private void MoveLocalY (float h)
		{
			// Debug.Log ($"{(Target?Target.position:Vector3.zero)}");
			if (!Target) return;
			// Target.MovePosition ();
			Target.velocity = parent.TransformPoint (Vector3.up * h) - Target.position;
		}
	}
	public enum InputControlType
	{
		Center = 0,
		Side = 1
	}
}
