using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class OnDisabeUnityEvent : MonoBehaviour
    {

        [SerializeField] UnityEvent onDisable;
        [SerializeField] bool debug;

        private void OnDisable ()
        {
            onDisable.Invoke ();
            if (debug) Debug.Log ($"{("OnEnable".a())} {name.mb()}\n{onDisable.Log()}", this);
        }
    }
}
