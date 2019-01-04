using GameCore;

using UnityEngine;
namespace CyberBeat
{
    public class GameEventListenerColorInterractor : AGameEventListenerUnityOject<ColorInterractor>
	{
		[SerializeField] EventListenerColorInterractor _listener;
		protected override AEventListenerUnityObject<ColorInterractor> listener { get { return _listener; } }

	}
}
