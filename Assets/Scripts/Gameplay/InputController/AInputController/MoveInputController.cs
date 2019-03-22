using DG.Tweening;

using GameCore;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
	public abstract class MoveInputController : AInputController
	{
		[SerializeField] protected MoveInputSettings mySettings => (MoveInputSettings) settings;

	}
}
