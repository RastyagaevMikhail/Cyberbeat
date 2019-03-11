using GameCore;

using Sirenix.OdinInspector;

using System.Security.Cryptography.X509Certificates;

using UnityEngine;
namespace CyberBeat
{
	[CreateAssetMenu (fileName = "CenterBotInutController", menuName = "CyberBeat/Bot/InputController/Center")]
	public class CenterBotInutController : BotInputController
	{
		[SerializeField] MoveInputSettings inputSettings;
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
			bool isBrickMyColor = colorInterractor.gameObject.CompareTag ("Brick") && matSwitch.ChechColor (colorInterractor.CurrentColor);

			if (isBrickMyColor)
			{
				Vector3 dir = colorInterractor.position - _target.position;

				dir = _target.InverseTransformDirection (dir);

				bool isBit = dir.sqrMagnitude <= SqrWidth;

				if (isBit)
				{
					if (dir.x > 0)
						inputControllerComponent.TapRight ();
					else if (dir.x < 0)
						inputControllerComponent.TapLeft ();

					if (dir.x != 0) StartMove = true;
				}
			}
		}

	}
}
