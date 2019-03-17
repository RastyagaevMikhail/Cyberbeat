namespace GameCore
{
    using System;

    using UnityEngine;

    public class GameEventListenerRewradVideo : MonoBehaviour
    {
        [SerializeField] GameEventRewardVideo Event;
        [SerializeField] UnityEventRewardVideo Responce;

        private void OnEnable ()
        {
            Event.RegisterListener (this);
        }
        private void OnDisable ()
        {
            Event.UnRegisterListener (this);
        }

        internal void OnEventRaised (double amount, string name)
        {
            Responce.Invoke (amount, name);
        }
    }
}
