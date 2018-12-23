using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class OnDisabeUnityEvent : MonoBehaviour
    {
        [DrawWithUnity]
        [SerializeField] UnityEvent onDisable;
        private void OnDisable ()
        {
            onDisable.Invoke ();
        }
    }
}
