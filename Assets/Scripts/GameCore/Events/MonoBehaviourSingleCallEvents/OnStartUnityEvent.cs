using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class OnStartUnityEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent OnStart;
        [SerializeField] bool debug;

        private void Start ()
        {
            if (debug)
                Debug.Log ($"{("OnStart").a()} {name.mb()}", this);
            OnStart.Invoke ();
        }
    }
}
