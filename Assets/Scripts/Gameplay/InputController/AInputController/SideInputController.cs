using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	[CreateAssetMenu (fileName = "CenterInputController", menuName = "CyberBeat/InputController/Side")]
	public class SideInputController : MoveInputController
	{
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
		float width => mySettings.width;
		public void MoveRight ()
		{
			rigth = false;
			Target.DOLocalMoveX (width, duration);
		}
		public void MoveLeft ()
		{
			rigth = true;
			Target.DOLocalMoveX (-width, duration);
		}

	}
}
