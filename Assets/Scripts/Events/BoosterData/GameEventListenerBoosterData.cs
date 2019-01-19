using GameCore;

using UnityEngine;
namespace CyberBeat
{
	public class GameEventListenerBoosterData : AGameEventListenerUnityOject<BoosterData, EventListenerBoosterData>
	{
		[SerializeField] EventListenerBoosterData _listener;
		protected override AEventListenerUnityObject<BoosterData, EventListenerBoosterData> listener { get { return _listener; } }
	}
}
