using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	[CreateAssetMenu (fileName = "CenterInputController", menuName = "CyberBeat/InputController/Side")]
	public class SideInputController : MoveInputController
	{
		public override void Awake ()
		{
			rightPath = new Vector3[] { Vector3.right * mySettings.width };
			leftPath = new Vector3[] { Vector3.left * mySettings.width };
		}
		float duration { get { return mySettings.SwipeDuration / 2f; } }
		bool rigth;

		public override void TapRight () => Tap ();
		public override void TapLeft () => Tap ();
		public void Tap ()
		{
			if (rigth)
				MoveRight ();
			else
				MoveLeft ();
		}
		public void MoveRight ()
		{
			rigth = false;
			Target.DOLocalPath (rightPath, duration);
		}
		public void MoveLeft ()
		{
			rigth = true;
			Target.DOLocalPath (leftPath, duration);
		}

	}
}
