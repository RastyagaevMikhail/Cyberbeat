using Sirenix.OdinInspector;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class GameEventListeners : MonoBehaviour
    {
        [SerializeField] List<EventListener> listeners;
        private void OnEnable ()
        {
            foreach (var listener in listeners)
                listener.OnEnable ();
        }

        private void OnDisable ()
        {
            foreach (var listener in listeners)
                listener.OnDisable ();
        }
    }
}
