using Sirenix.OdinInspector;

using System;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public abstract class AGameEventListenerUnityOject<TObject> : SerializedMonoBehaviour
    where TObject : UnityEngine.Object
    {
        [SerializeField] protected abstract AEventListenerUnityObject<TObject> listener { get; }

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
