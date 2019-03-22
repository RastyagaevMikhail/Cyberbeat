using DG.Tweening;

using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	[CreateAssetMenu (fileName = "CenterInputController", menuName = "CyberBeat/InputController/Center")]
	public class CenterInputController : MoveInputController
	{
		float duration { get { return mySettings.SwipeDuration / 2f; } }
	
		float width => mySettings.width;
		Ease easeMove => mySettings.easeMove;
		public override void TapRight ()
		{
			Target
				.DOLocalMoveX (width, duration)
				.SetEase (easeMove)
				.OnComplete (() =>
					Target.DOLocalMoveX (0, duration)
					.SetEase (easeMove));
		}
		public override void TapLeft ()
		{
			Target
				.DOLocalMoveX (-width, duration)
				.SetEase (easeMove)
				.OnComplete (() =>
					Target.DOLocalMoveX (0, duration)
					.SetEase (easeMove));
		}
	}
}
