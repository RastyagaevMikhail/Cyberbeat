using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{

    [CreateAssetMenu (fileName = "GameEvent", menuName = "Events/GameCore/GameEvent/void")]
    public class GameEvent : ScriptableObject
    {
        [SerializeField]
        public List<EventListener> eventListeners = new List<EventListener> ();

        public void Raise ()
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised ();
        }

        public virtual void RegisterListener (EventListener listener)
        {
            if (!eventListeners.Contains (listener))
                eventListeners.Add (listener);
        }

        public virtual void UnRegisterListener (EventListener listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
#if UNITY_EDITOR
        [ContextMenu ("Show path")] public void ShowPath () { Debug.Log (UnityEditor.AssetDatabase.GetAssetPath (this)); }
#endif
    }

}
