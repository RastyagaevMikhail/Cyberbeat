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
            if (debug)
                Debug.Log ("OnDisable {0}".AsFormat (this), this);
            onDisable.Invoke ();
        }
    }
}
