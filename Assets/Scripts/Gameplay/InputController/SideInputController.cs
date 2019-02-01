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
			rightPath = new Vector3[] { Vector3.right };
			leftPath = new Vector3[] { Vector3.left };
		}
		float duration { get { return settings.SwipeDuration / 2f; } }

		public override void MoveRight ()
		{
			Target.DOLocalPath (rightPath, duration);
		}
		public override void MoveLeft ()
		{
			Target.DOLocalPath (leftPath, duration);
		}

	}
}
