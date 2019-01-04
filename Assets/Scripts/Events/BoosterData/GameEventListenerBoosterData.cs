using GameCore;

using UnityEngine;
namespace CyberBeat
{
	public class GameEventListenerBoosterData : AGameEventListenerUnityOject<BoosterData>
	{
		[SerializeField] EventListenerBoosterData _listener;
		protected override AEventListenerUnityObject<BoosterData> listener { get { return _listener; } }
	}
}
