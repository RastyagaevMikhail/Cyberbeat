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
            if (debug)
                Debug.Log ("OnEnable {0}".AsFormat (this), this);
            onEnable.Invoke ();
        }
    }
}
