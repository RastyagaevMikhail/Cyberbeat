﻿using Sirenix.OdinInspector;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{

    [CreateAssetMenu (fileName = "GameEvent", menuName = "Events/GameCore/GameEvent/void")]
    public class GameEvent : ScriptableObject
    {
        [SerializeField]
        public List<EventListener> eventListeners = new List<EventListener> ();

        [DisableInEditorMode]
        [Button]
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

        public virtual void UnRegisterListener (EventListener listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
    }

}

