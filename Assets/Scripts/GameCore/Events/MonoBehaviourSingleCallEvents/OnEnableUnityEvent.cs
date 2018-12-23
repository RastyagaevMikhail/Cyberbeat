using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class OnEnableUnityEvent : MonoBehaviour
    {
        [DrawWithUnity]
        [SerializeField] UnityEvent onEnable;
        private void OnEnable ()
        {
            onEnable.Invoke ();
        }
    }
}
