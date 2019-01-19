using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class OnDisabeUnityEvent : MonoBehaviour
    {

        [SerializeField] UnityEvent onDisable;
        private void OnDisable ()
        {
            onDisable.Invoke ();
        }
    }
}
