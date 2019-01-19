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
<<<<<<< HEAD
            if (debug)
                Debug.Log ("OnAwake {0}".AsFormat (name), this);
=======
>>>>>>> parent of 46a173b... Fix Some Problem with Booster and Memory
            OnAwake.Invoke ();
        }
    }
}
