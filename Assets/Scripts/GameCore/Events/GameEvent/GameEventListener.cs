using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] GameEvent Event;
        [SerializeField] UnityEvent Responce;
        [SerializeField] bool debug;

        public void OnEventRaised ()
        {
            Responce.Invoke ();
            if (debug) Debug.Log ($"{("OnEvent".a())} {Event.name.so()} {("Raised").a()}\n{Responce.Log()}");
        }
        public void OnEnable ()
        {
            Event.RegisterListener (this);
        }

        public void OnDisable ()
        {
            Event.UnRegisterListener (this);
        }
    }
}
