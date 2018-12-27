using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable]
    public class EventListenerRewardVideo
    {
        [SerializeField] GameEventRewardVideo Event;
        [SerializeField] RewardVideoUnityEvent Responce;

        public EventListenerRewardVideo (GameEventRewardVideo startEvent, UnityAction<double, string> action)
        {
            Event = startEvent;
            Responce = new RewardVideoUnityEvent ();
            Responce.AddListener (action);

        }
        public void OnEventRaised (double amount, string _name)
        {
            Responce.Invoke (amount, _name);
        }
        public bool OnEnable ()
        {
            bool IfNotNullEvent = Event != null;
            if (IfNotNullEvent)
                Event.RegisterListener (this);
            return IfNotNullEvent;

        }

        public bool OnDisable ()
        {
            bool IfNotNullEvent = Event != null;
            if (IfNotNullEvent)
                Event.UnRegisterListener (this);
            return IfNotNullEvent;
        }
    }
}
