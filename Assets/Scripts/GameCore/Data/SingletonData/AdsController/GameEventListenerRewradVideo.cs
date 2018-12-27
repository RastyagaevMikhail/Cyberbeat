namespace GameCore
{

    using UnityEngine;

    public class GameEventListenerRewradVideo : MonoBehaviour
    {
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
