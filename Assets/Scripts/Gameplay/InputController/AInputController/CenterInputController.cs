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
		Transform transform;
		public override void Awake ()
		{
			rightPathMove = new Vector3[] { Vector3.zero, Vector3.right * mySettings.width };
			rightPathReturn = new Vector3[] { Vector3.right * mySettings.width, Vector3.zero };
			leftPathMove = new Vector3[] { Vector3.zero, Vector3.left * mySettings.width };
			leftPathReturn = new Vector3[] { Vector3.left * mySettings.width, Vector3.zero };
		}
		float width => mySettings.width;
		Ease easeMove => mySettings.easeMove;
		public override void TapRight ()
		{
			Target.transform
				.DOLocalMoveX (width, duration)
				.SetEase (easeMove)
				.OnComplete (() =>
					Target.transform.DOLocalMoveX (0, duration)
					.SetEase (easeMove));
		}
		public override void TapLeft ()
		{
			Target.transform
				.DOLocalMoveX (-width, duration)
				.SetEase (easeMove)
				.OnComplete (() =>
					Target.transform.DOLocalMoveX (0, duration)
					.SetEase (easeMove));
		}
	}
}
