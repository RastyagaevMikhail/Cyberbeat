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
		public override void Awake ()
		{
			rightPath = new Vector3[] { Vector3.right * mySettings.width, Vector3.zero };
			leftPath = new Vector3[] { Vector3.left * mySettings.width, Vector3.zero };
		}
		public override void TapRight ()
		{
			Target.DOLocalPath (rightPath, duration).SetEase(mySettings.easeMove);
		}
		public override void TapLeft ()
		{
			Target.DOLocalPath (leftPath, duration).SetEase(mySettings.easeMove);
		}
	}
}
