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
        [SerializeField] List<EventListenerContainer> containers;
        private void OnEnable ()
        {
            foreach (var container in containers)
                container.Listener.OnEnable ();
        }

        private void OnDisable ()
        {
            foreach (var container in containers)
                container.Listener.OnDisable ();
        }
    }
}
