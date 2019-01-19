using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{

    [CreateAssetMenu (
        fileName = "GameEventTimeEvent", 
        menuName = "CyberBeat/GameEvent/TimeEvent")]
    public class GameEventTimeEvent : ScriptableObject
    {
        [SerializeField]
        public List<GameEventListenerTimeEvent> eventListeners = new List<GameEventListenerTimeEvent> ();

        public void Raise (TimeEvent arg)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised (arg);
        }

        public virtual void RegisterListener (GameEventListenerTimeEvent listener)
        {
            if (!eventListeners.Contains (listener))
                eventListeners.Add (listener);
        }

        public virtual void UnRegisterListener (GameEventListenerTimeEvent listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
#if UNITY_EDITOR
        [ContextMenu ("Show path")] public void ShowPath () { Debug.Log (UnityEditor.AssetDatabase.GetAssetPath (this)); }
#endif
    }

}
