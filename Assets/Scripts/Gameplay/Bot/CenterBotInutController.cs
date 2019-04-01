using GameCore;

using Sirenix.OdinInspector;

using System.Security.Cryptography.X509Certificates;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
	[CreateAssetMenu (fileName = "CenterBotInutController", menuName = "CyberBeat/Bot/InputController/Center")]
	public class CenterBotInutController : BotInputController
	{
		private const string BrickTag = "Brick";
		[SerializeField] MoveInputSettings inputSettings;
		[SerializeField] UnityEvent onRigth;
		[SerializeField] UnityEvent onLeft;
		public override float Speed { set { } }
		Transform _target;
		float SqrWidth;
		public override void Initialize (Transform target)
		{
			_target = target;
			SqrWidth = inputSettings.width.Sqr ();
		}

		public override void UpdatePosition ()
		{
			nearBeats.ForEach (ForEachNear);
		}
		public override void ForEachNear (ColorInterractor colorInterractor)
		{
			if (StartMove) return;
			bool isBrickMyColor = colorInterractor.gameObject.CompareTag (BrickTag) && matSwitch.ChechColor (colorInterractor.CurrentColor);

			if (isBrickMyColor)
			{
				Vector3 dir = colorInterractor.position - _target.position;

				dir = _target.InverseTransformDirection (dir);

				bool isBit = dir.sqrMagnitude <= SqrWidth;

				if (isBit)
				{
					if (dir.x > 0)
						onRigth.Invoke ();
					else if (dir.x < 0)
						onLeft.Invoke ();

					if (dir.x != 0) StartMove = true;
				}
			}
		}

	}
}
