using DG.Tweening;

using GameCore;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
	public abstract class MoveInputController : AInputController
	{
		protected Vector3[] rightPathMove;
		protected Vector3[] rightPathReturn;
		protected Vector3[] leftPathMove;
		protected Vector3[] leftPathReturn;

		[SerializeField] protected MoveInputSettings mySettings => (MoveInputSettings) settings;

	}
}
