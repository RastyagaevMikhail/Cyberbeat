using DG.Tweening;

using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

using UnityEngine;
namespace CyberBeat
{
	[CreateAssetMenu (fileName = "CenterInputController", menuName = "CyberBeat/InputController/Center")]
	public class CenterInputController : MoveInputController
	{
		float duration { get { return mySettings.SwipeDuration / 2f; } }

		float width => mySettings.width;
		Ease easeMove => mySettings.easeMove;
		[SerializeField] bool debug;
		public override void TapRight ()
		{
			if (debug) Debug.Log ("CenterInputController.TapRight");
			Target
				.DOLocalMoveX (width, duration)
				.SetEase (easeMove)
				.OnComplete (() =>
					Target.DOLocalMoveX (0, duration)
					.SetEase (easeMove));
		}
		public override void TapLeft ()
		{
			if (debug) Debug.Log ("CenterInputController.TapLeft");
			Target
				.DOLocalMoveX (-width, duration)
				.SetEase (easeMove)
				.OnComplete (() =>
					Target.DOLocalMoveX (0, duration)
					.SetEase (easeMove));
		}
	}
}
