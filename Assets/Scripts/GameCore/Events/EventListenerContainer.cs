namespace GameCore
{
    using UnityEngine;

    [CreateAssetMenu (fileName = "Listener Container", menuName = "Events/GameCore/Listener")]
    public class EventListenerContainer : ScriptableObject
    {
        public EventListener Listener;
    }
}
