
using System;
using System.Collections.Generic;
using GameCore;
using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (
        fileName = "GameEventAdTypeString", 
        menuName = "CyberBeat/GameEvent_2/AdTypeString")]
    public class GameEventAdTypeString : ScriptableObject
    {
        [SerializeField]
        public List<GameEventListenerAdTypeString> eventListeners = new List<GameEventListenerAdTypeString> ();

        public void Raise (AdType arg1, String arg2)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised (arg1, arg2);
        }

        public virtual void RegisterListener (GameEventListenerAdTypeString listener)
        {
            if (!eventListeners.Contains (listener))
                eventListeners.Add (listener);
        }

        public virtual void UnRegisterListener (GameEventListenerAdTypeString listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
    }

}
