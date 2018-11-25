using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{

    [CreateAssetMenu(fileName = "GameEvent" , menuName ="Events/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        [SerializeField]
        private List<EventListener> eventListeners =
            new List<EventListener> ();

        public void Raise ()
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised ();
        }

        public virtual void RegisterListener (EventListener listener)
        {
            if (!eventListeners.Contains (listener))
                eventListeners.Add (listener);
        }

        public virtual void UnregisterListener (EventListener listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
    }
}
