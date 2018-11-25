using Sirenix.OdinInspector;

using System;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] EventListener listener;

        private void OnEnable ()
        {
            if (!listener.OnEnable ())
            {
                Debug.LogError ("Event not set On listener", this);
            }
        }

        private void OnDisable ()
        {
            if (!listener.OnDisable ())
            {
                Debug.LogError ("Event not set On listener", this);
            }
        }
    }

    [Serializable]
    public class EventListener
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
