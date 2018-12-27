using System;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public abstract class GameEventListenerStruct<TStruct> : MonoBehaviour
    where TStruct : struct
    {
        [SerializeField] EventListenerStruct<TStruct> listener;

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
}
