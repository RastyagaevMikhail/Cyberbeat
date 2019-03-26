using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class OnApplicationQuitUnityEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent onApplicationQuit;
        private void OnApplicationQuit ()
        {
            onApplicationQuit.Invoke ();
        }
    }
}
