using UnityEngine;
namespace GameCore
{
    public class OnApplicationPauseUnityEvent : MonoBehaviour
    {
        [SerializeField] UnityEventBool onApplicationPause;
        [SerializeField] bool debug;
        string log = string.Empty;
        private void OnApplicationPause (bool pauseStatus)
        {
            onApplicationPause.Invoke (pauseStatus);
            if (debug)
            {
                log += $"{@"OnApplicationPause".a()} {name}\n";
                log += $"{onApplicationPause.Log()}\n";
                Debug.Log (log);
            }
        }
    }
}
