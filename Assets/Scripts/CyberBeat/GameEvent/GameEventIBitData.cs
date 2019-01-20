using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{

    [CreateAssetMenu (
        fileName = "GameEventIBitData", 
        menuName = "CyberBeat/GameEvent/IBitData")]
    public class GameEventIBitData : ScriptableObject
    {
        [SerializeField]
        public List<GameEventListenerIBitData> eventListeners = new List<GameEventListenerIBitData> ();

        public void Raise (IBitData arg)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised (arg);
        }

        public virtual void RegisterListener (GameEventListenerIBitData listener)
        {
            if (!eventListeners.Contains (listener))
                eventListeners.Add (listener);
        }

        public virtual void UnRegisterListener (GameEventListenerIBitData listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
#if UNITY_EDITOR
        [ContextMenu ("Show path")] public void ShowPath () { Debug.Log (UnityEditor.AssetDatabase.GetAssetPath (this)); }
#endif
    }

}
