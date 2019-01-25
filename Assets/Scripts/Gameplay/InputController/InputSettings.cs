using DG.Tweening;
using UnityEngine;
	

namespace CyberBeat
{
	[CreateAssetMenu(fileName = "InputSettings", menuName = "CyberBeat/InputSettings", order = 0)]
	public class InputSettings : ScriptableObject {
		
		public DG.Tweening.Ease easeMove = Ease.OutQuint;
		public float SwipeDuration = 0.2f;
		public float width = 2.35f;
	}
}