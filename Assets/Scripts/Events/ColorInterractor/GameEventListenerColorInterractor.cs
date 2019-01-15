using GameCore;

using UnityEngine;
namespace CyberBeat
{
	public class GameEventListenerColorInterractor : AGameEventListenerUnityOject<ColorInterractor, EventListenerColorInterractor>
	{
		[SerializeField] EventListenerColorInterractor _listener;
		protected override AEventListenerUnityObject<ColorInterractor, EventListenerColorInterractor> listener { get { return _listener; } }

	}
}
