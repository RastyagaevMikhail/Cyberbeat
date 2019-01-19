using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{

    [CreateAssetMenu (
        fileName = "InputControlTypeGameEvent", 
        menuName = "CyberBeat/GameEvent/InputControlType")]
    public class InputControlTypeGameEvent : ScriptableObject
    {
        [SerializeField]
        public List<GameEventListenerInputControlType> eventListeners = new List<GameEventListenerInputControlType> ();

        public void Raise (InputControlType arg)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised (arg);
        }

        public virtual void RegisterListener (GameEventListenerInputControlType listener)
        {
            if (!eventListeners.Contains (listener))
                eventListeners.Add (listener);
        }

        public virtual void UnRegisterListener (GameEventListenerInputControlType listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
#if UNITY_EDITOR
        [ContextMenu ("Show path")] public void ShowPath () { Debug.Log (UnityEditor.AssetDatabase.GetAssetPath (this)); }
#endif
    }

}
