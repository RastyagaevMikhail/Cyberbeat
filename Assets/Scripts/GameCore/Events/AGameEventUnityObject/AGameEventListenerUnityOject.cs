using System;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public abstract class AGameEventListenerUnityOject<TObject, TEventListener> : MonoBehaviour
    where TObject : UnityEngine.Object
    where TEventListener : AEventListenerUnityObject<TObject, TEventListener>
    {
        [SerializeField] protected abstract AEventListenerUnityObject<TObject, TEventListener> listener { get; }

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
