using GameCore;

using UnityEngine;

namespace CyberBeat
{
	[CreateAssetMenu (fileName = "InputControlTypeEqualsCondition", menuName = "CyberBeat/Condition/Equals/InputControlType")]
	public class InputControlTypeEqualsCondition : ACondition
	{
		[SerializeField] InputControlType value;
		[SerializeField] InputControlTypeVariable variable;
		public override bool Value => variable.Value.Equals(value);
	}
}
