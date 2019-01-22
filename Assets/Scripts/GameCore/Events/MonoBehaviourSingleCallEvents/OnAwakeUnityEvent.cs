using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
            OnAwake.Invoke ();
            {
              
                if (debug) Debug.Log ($"{("OnAwake").a()} {name.mb()}\n{OnAwake.Log()}", this);
            }
        }
    }
}
