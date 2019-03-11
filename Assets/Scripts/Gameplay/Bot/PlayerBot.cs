using GameCore;

using Sirenix.OdinInspector;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
	[RequireComponent (typeof (GameEventListenerFloat))] //  UpdateSpeed (float speed)
	public class PlayerBot : MonoBehaviour
	{
		[SerializeField] InputControlTypeBotInputControllerSelector selector;
		[SerializeField] BotInputController currentController;
		[SerializeField] TransformVariable targetTransformVariable;
		[SerializeField] Transform target => targetTransformVariable.Value;

		public float Speed { set => currentController.Speed = value; }

		public void SetInputControlType (InputControlType inputControlType) =>
			currentController = selector[inputControlType];
		private void Start () =>
			selector.Values.ForEach (ctr => ctr.Initialize (target));
		private void Update ()
		{
			currentController.UpdatePosition ();
		}

		public void onBit ()
		{
			currentController.StartMove = false;
		}
	}
}
