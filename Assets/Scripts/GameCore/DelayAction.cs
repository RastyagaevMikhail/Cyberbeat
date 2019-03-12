using System.Collections;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public class DelayAction : MonoBehaviour
    {
        [SerializeField] bool asDeltaTime;
        [SerializeField] float delay;
        [SerializeField] UnityEvent action;
        [SerializeField] bool debug;

        [ContextMenu ("Start Invoke DelayedAction")]
        public void StartInvokeDelayedAction ()
        {
            Invoke ("IvokeDealayAction", asDeltaTime ? delay * Time.deltaTime : delay);
            if (debug) Debug.Log ($"{("OnInvoke".black())}\n{action.Log()}", this);
        }
          public void StartInvokeDelayedAction (float timeDelay)
        {
            Invoke ("IvokeDealayAction", timeDelay);
            if (debug) Debug.Log ($"{("OnInvoke".black())}\n{action.Log()}", this);
        }

        [ContextMenu ("Ivoke DealayAction")]
        void IvokeDealayAction ()
        {
            action.Invoke ();
            if (debug) Debug.Log ($"{("DelayAction".black())}\n{action.Log()}", this);
        }
    }
}
