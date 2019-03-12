using GameCore;

using UnityEngine;

namespace CyberBeat
{
	[CreateAssetMenu (fileName = "MenuWindowTypeCondition", menuName = "CyberBeat/Condition/MenuWindowType")]
	public class MenuWindowTypeCondition : ACondition
	{
		[SerializeField] MenuWindowType windowType;
		[SerializeField] MenuWindowTypeVariable menuWindowTypeVariable;
		public override bool Value => windowType.Equals (menuWindowTypeVariable.Value);
	}
}
