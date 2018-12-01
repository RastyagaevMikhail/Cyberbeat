using DG.Tweening;

using UnityEngine;
namespace CyberBeat
{
	public interface IInputController
	{
		void MoveRight ();
		void MoveLeft ();
		void Init (Transform transform, InputSettings inputSettings);
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
