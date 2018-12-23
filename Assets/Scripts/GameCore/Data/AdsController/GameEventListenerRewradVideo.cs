namespace GameCore
{
    using Sirenix.OdinInspector;

    using UnityEngine;

    public class GameEventListenerRewradVideo : MonoBehaviour
    {
        [DrawWithUnity]
        [SerializeField] EventListenerRewardVideo listener;

        private void OnEnable ()
        {
            listener.OnEnable ();
        }
        private void OnDisable ()
        {
            listener.OnDisable ();
        }
    }
}
