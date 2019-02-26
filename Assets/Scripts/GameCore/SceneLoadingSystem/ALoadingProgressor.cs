using UnityEngine;

namespace GameCore
{
	public abstract class ALoadingProgressor : MonoBehaviour, ILoadingProgressor
	{
		public abstract float Value { get; set; }
	}
}
