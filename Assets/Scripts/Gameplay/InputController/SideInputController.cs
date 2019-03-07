using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	[CreateAssetMenu (fileName = "CenterInputController", menuName = "CyberBeat/InputController/Side")]
	public class SideInputController : AInputController
	{
		public override void Awake ()
		{
			rightPath = new Vector3[] { Vector3.right * settings.width };
			leftPath = new Vector3[] { Vector3.left * settings.width };
		}
		float duration { get { return settings.SwipeDuration / 2f; } }
		bool rigth;
		public override void Tap ()
		{
			if (rigth)
				MoveRight ();
			else
				MoveLeft ();
		}

		public override void MoveRight ()
		{
			rigth = false;
			Target.DOLocalPath (rightPath, duration);
		}
		public override void MoveLeft ()
		{
			rigth = true;
			Target.DOLocalPath (leftPath, duration);
		}

	}
}
