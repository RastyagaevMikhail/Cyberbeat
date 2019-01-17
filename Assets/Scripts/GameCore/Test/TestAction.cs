using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public class TestAction : MonoBehaviour
    {
        [SerializeField] UnityEvent action;

        public void Invoke ()
        {
            action.Invoke ();
        }
    }
}
