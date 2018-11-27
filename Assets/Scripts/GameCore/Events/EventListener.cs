using Sirenix.OdinInspector;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [CreateAssetMenu(fileName= "Listener Container", menuName = "Events/GameCore/Listener")]
    public class EventListener : ScriptableObject
    {
        [Tooltip ("Event to register with.")]
        public GameEvent Event;
        [Tooltip ("Response to invoke when Event is raised.")]
        [DrawWithUnity] public UnityEvent Response;

        public EventListener (GameEvent startEvent, UnityAction action)
        {
            Event = startEvent;
            Response = new UnityEvent ();
            Response.AddListener (action);

        }

        public void OnEventRaised ()
        {
            Response.Invoke ();
        }
        public bool OnEnable ()
        {
            if (Event)
                Event.RegisterListener (this);
            return Event;

        }

        public bool OnDisable ()
        {
            if (Event)
                Event.UnregisterListener (this);
            return Event;

        }
    }
}
