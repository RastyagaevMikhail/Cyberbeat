using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{

    [CreateAssetMenu (
        fileName = "SpeedTimeDataGameEvent", 
        menuName = "CyberBeat/GameEvent/SpeedTimeData")]
    public class SpeedTimeDataGameEvent : ScriptableObject
    {
        [SerializeField]
        public List<SpeedTimeDataGameEventListener> eventListeners = new List<SpeedTimeDataGameEventListener> ();

        public void Raise (SpeedTimeData arg)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised (arg);
        }

        public virtual void RegisterListener (SpeedTimeDataGameEventListener listener)
        {
            if (!eventListeners.Contains (listener))
                eventListeners.Add (listener);
        }

        public virtual void UnRegisterListener (SpeedTimeDataGameEventListener listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
#if UNITY_EDITOR
        [ContextMenu ("Show path")] public void ShowPath () { Debug.Log (UnityEditor.AssetDatabase.GetAssetPath (this)); }
#endif
    }

}
