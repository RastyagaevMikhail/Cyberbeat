using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class OnStartUnityEvent : MonoBehaviour
    {
        [DrawWithUnity]
        [SerializeField] UnityEvent OnStart;
        private void Start ()
        {
            OnStart.Invoke ();
        }
    }
}
