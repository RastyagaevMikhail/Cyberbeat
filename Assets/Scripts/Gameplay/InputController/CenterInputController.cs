using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	[CreateAssetMenu (fileName = "CenterInputController", menuName = "CyberBeat/InputController/Center")]
	public class CenterInputController : AInputController
	{

		float duration { get { return settings.SwipeDuration / 2f; } }
		Rigidbody rb;
		public override void Awake ()
		{
			rightPath = new Vector3[] { Vector3.right * settings.width, Vector3.zero };
			leftPath = new Vector3[] { Vector3.left * settings.width, Vector3.zero };
		}

		//Call In Update
		
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
