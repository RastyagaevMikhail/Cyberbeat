using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class OnEnableUnityEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent onEnable;
        [SerializeField] bool debug;

        private void OnEnable ()
        {
            onEnable.Invoke ();
            if (debug) Debug.Log ($"{("OnEnable".a())} {name.mb()}\n{onEnable.Log()}", this);
        }
    }
}
