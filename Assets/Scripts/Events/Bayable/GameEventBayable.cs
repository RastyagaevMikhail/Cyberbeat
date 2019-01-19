using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
	[CreateAssetMenu (fileName = "GameEventBayable", menuName = "Events/CyberBeat/GameEvent/Bayable")]
	public class GameEventBayable : AGameEventUnityObject<ABayable, EventListenerBayable>
	{
		[SerializeField] List<EventListenerBayable> _eventListeners;
		protected override List<EventListenerBayable> eventListeners
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
