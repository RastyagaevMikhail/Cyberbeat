using System;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (
        fileName = "GameEventColorBool", 
        menuName = "CyberBeat/GameEvent_2/ColorBool")]
    public class GameEventColorBool : ScriptableObject
    {
        [SerializeField]
        public List<GameEventListenerColorBool> eventListeners = new List<GameEventListenerColorBool> ();

        public void Raise (Color arg1, bool arg2)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised (arg1, arg2);
        }

        public virtual void RegisterListener (GameEventListenerColorBool listener)
        {
            if (!eventListeners.Contains (listener))
                eventListeners.Add (listener);
        }

        public virtual void UnRegisterListener (GameEventListenerColorBool listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
    }

}
