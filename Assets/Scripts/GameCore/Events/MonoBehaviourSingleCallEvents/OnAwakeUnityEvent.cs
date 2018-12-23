using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class OnAwakeUnityEvent : MonoBehaviour
    {
        [DrawWithUnity]
        [SerializeField] UnityEvent OnAwake;
        private void Awake ()
        {
            OnAwake.Invoke ();
        }
    }
}
