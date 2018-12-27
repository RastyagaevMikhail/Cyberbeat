using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class OnEnableUnityEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent onEnable;
        private void OnEnable ()
        {
            onEnable.Invoke ();
        }
    }
}
