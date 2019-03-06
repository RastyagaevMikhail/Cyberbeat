using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class OnEnableOnDisableUnityEvent : MonoBehaviour
    {
        [SerializeField] bool debug;
        [SerializeField] UnityEvent onEnable;

        private void OnEnable ()
        {
            onEnable.Invoke ();
            if (debug) Debug.Log ($"{("OnEnable".a())} {name.mb()}\n{onEnable.Log()}", this);
        }

        [SerializeField] UnityEvent onDisable;

        private void OnDisable ()
        {
            onDisable.Invoke ();
            if (debug) Debug.Log ($"{("OnDisable".a())} {name.mb()}\n{onDisable.Log()}", this);
        }
    }
}
