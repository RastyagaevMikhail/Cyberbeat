using DG.Tweening;

using GameCore;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
	public abstract class AInputController : ScriptableObject
	{
		public abstract void TapRight ();
		public abstract void TapLeft ();

		[SerializeField] TransformVariable targetVariable;
		[SerializeField] protected InputSettings settings;
		protected Transform Target => targetVariable.Value;

	}
}
