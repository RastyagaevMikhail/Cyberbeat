using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class OnStartUnityEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent OnStart;
        private void Start ()
        {
            OnStart.Invoke ();
        }
    }
}
