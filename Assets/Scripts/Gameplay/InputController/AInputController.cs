using DG.Tweening;

using GameCore;

using UnityEngine;
namespace CyberBeat
{
	public abstract class AInputController : ScriptableObject
	{
		public abstract void MoveRight ();
		public abstract void MoveLeft ();
		[SerializeField] TransformVariable targetVariable;
		protected Transform Target { get { return targetVariable.ValueFast; } }
		[SerializeField] protected InputSettings settings;
	}
	public enum InputControlType
	{
		Center = 0,
		Side = 1
	}
}
