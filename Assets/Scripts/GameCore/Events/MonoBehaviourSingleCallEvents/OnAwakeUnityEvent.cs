using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class OnAwakeUnityEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent OnAwake;
        private void Awake ()
        {
            OnAwake.Invoke ();
        }
    }
}
