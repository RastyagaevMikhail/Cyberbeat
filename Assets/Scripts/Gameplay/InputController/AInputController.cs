using DG.Tweening;

using UnityEngine;
namespace CyberBeat
{
	public abstract class AInputController : ScriptableObject
	{
		public abstract void MoveRight ();
		public abstract void MoveLeft ();
		protected Transform Target;
		[SerializeField] protected InputSettings settings;
		public void Init (Transform target, InputSettings inputSettings)
		{
			Target = target;
			settings = inputSettings;
		}
	}
	public enum InputControlType
	{
		Center = 0,
		Side = 1
	}

	[System.Serializable]
	public class InputSettings
	{
		public Ease easeMove = Ease.OutQuint;
		public float SwipeDuration = 0.2f;
		public float width = 2.35f;
	}
}
