using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{

    [CreateAssetMenu (
        fileName = "GameEventColorInfoRuntimeSet", 
        menuName = "CyberBeat/GameEvent/ColorInfoRuntimeSet")]
    public class GameEventColorInfoRuntimeSet : ScriptableObject
    {
        [SerializeField]
        public List<GameEventListenerColorInfoRuntimeSet> eventListeners = new List<GameEventListenerColorInfoRuntimeSet> ();

        public void Raise (ColorInfoRuntimeSet arg)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised (arg);
        }

        public virtual void RegisterListener (GameEventListenerColorInfoRuntimeSet listener)
        {
            if (!eventListeners.Contains (listener))
                eventListeners.Add (listener);
        }

        public virtual void UnRegisterListener (GameEventListenerColorInfoRuntimeSet listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
#if UNITY_EDITOR
        [ContextMenu ("Show path")] public void ShowPath () { Debug.Log (UnityEditor.AssetDatabase.GetAssetPath (this)); }
#endif
    }

}
