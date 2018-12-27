using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu(fileName =  "GameEventRewardVideo",menuName = "Events/AdsController/RewardVideo")]
	public class GameEventRewardVideo : ScriptableObject
	{
		[SerializeField]
		public List<EventListenerRewardVideo> eventListeners = new List<EventListenerRewardVideo> ();

		public void Raise (double amount, string name)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (amount,name);
		}

		public virtual void RegisterListener (EventListenerRewardVideo listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerRewardVideo listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}
	}
}