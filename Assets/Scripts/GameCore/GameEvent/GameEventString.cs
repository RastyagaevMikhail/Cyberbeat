using System;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (
        fileName = "GameEventString", 
        menuName = "GameCore/GameEvent/String")]
    public class GameEventString : ScriptableObject
    {
        [SerializeField]
        public List<GameEventListenerString> eventListeners = new List<GameEventListenerString> ();

        public void Raise (String arg)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised (arg);
        }

        public virtual void RegisterListener (GameEventListenerString listener)
        {
            if (!eventListeners.Contains (listener))
                eventListeners.Add (listener);
        }

        public virtual void UnRegisterListener (GameEventListenerString listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
#if UNITY_EDITOR
        [ContextMenu ("Show path")] public void ShowPath () { Debug.Log (UnityEditor.AssetDatabase.GetAssetPath (this)); }
#endif
    }

}
