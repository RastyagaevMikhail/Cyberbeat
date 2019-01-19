using GameCore;

using UnityEngine;
namespace CyberBeat
{
	public class GameEventListenerBayable : AGameEventListenerUnityOject<ABayable>
	{
		[SerializeField] EventListenerBayable _listener;
		protected override AEventListenerUnityObject<ABayable> listener { get { return _listener; } }
	}
}
