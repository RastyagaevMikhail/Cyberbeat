using DG.Tweening;

using GameCore;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
	public abstract class MoveInputController : AInputController
	{
		protected Vector3[] rightPath;
		protected Vector3[] leftPath;

		[SerializeField] protected MoveInputSettings mySettings => (MoveInputSettings) settings;

	}
}
