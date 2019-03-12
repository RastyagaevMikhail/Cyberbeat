using DG.Tweening;

using GameCore;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
	public abstract class AInputController : ScriptableObject
	{
		public abstract void Awake ();
		public abstract void TapRight ();
		public abstract void TapLeft ();

		[SerializeField] RigidbodyVariable targetVariable;
		[SerializeField] protected InputSettings settings;
		protected Rigidbody Target => targetVariable.Value;

	}
}
