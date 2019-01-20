using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class OnAwakeUnityEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent OnAwake;
        [SerializeField] bool debug;
        private void Awake ()
        {
            if (debug)
                Debug.Log ($"{("OnAwake").a()} {name.mb()}", this);
            OnAwake.Invoke ();
        }
    }
}
