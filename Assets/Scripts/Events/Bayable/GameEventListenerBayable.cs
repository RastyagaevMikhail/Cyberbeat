using GameCore;

using UnityEngine;
namespace CyberBeat
{
	public class GameEventListenerBayable : AGameEventListenerUnityOject<ABayable, EventListenerBayable>
	{
		[SerializeField] EventListenerBayable _listener;
		protected override AEventListenerUnityObject<ABayable, EventListenerBayable> listener
		{
			get
			{
				return _listener;
			}
		}
	}
}
