using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
	[CreateAssetMenu (fileName = "GameEventColorInterractor", menuName = "Events/CyberBeat/GameEvent/ColorInterractor")]
	public class GameEventColorInterractor : AGameEventUnityObject<ColorInterractor, EventListenerColorInterractor>
	{
		[SerializeField] List<EventListenerColorInterractor> _eventListeners;
		protected override List<EventListenerColorInterractor> eventListeners
		{
			get
			{
				return _eventListeners;
			}
			set
			{
				_eventListeners = value;
			}
		}
	}
}
