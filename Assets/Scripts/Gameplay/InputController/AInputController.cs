using DG.Tweening;

using GameCore;

using UnityEngine;
namespace CyberBeat
{
	public abstract class AInputController : ScriptableObject
	{
		public abstract void Awake ();
		public abstract void MoveRight ();
		public abstract void MoveLeft ();
		protected Vector3[] rightPath;
		protected Vector3[] leftPath;
		[SerializeField] RigidbodyVariable targetVariable;
		protected Rigidbody Target { get { return targetVariable.ValueFast; } }
		protected MonoBehaviour monoTarget = null;
		protected MonoBehaviour MonoTarget => monoTarget ?? (monoTarget = Target.GetComponent<MonoBehaviour> ());
		[SerializeField] protected InputSettings settings;
	}
	public enum InputControlType
	{
		Center = 0,
		Side = 1
	}
}
