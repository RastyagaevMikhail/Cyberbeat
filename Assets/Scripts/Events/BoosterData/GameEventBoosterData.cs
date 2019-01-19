using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
	[CreateAssetMenu (fileName = "GameEventBoosterData", menuName = "Events/CyberBeat/GameEvent/BoosterData")]
	public class GameEventBoosterData : AGameEventUnityObject<BoosterData, EventListenerBoosterData>
	{
		[SerializeField] List<EventListenerBoosterData> _eventListeners;
		protected override List<EventListenerBoosterData> eventListeners
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
