using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{

    [CreateAssetMenu (
        fileName = "GameEventAudioClip", 
        menuName = "GameCore/GameEvent/AudioClip")]
    public class GameEventAudioClip : ScriptableObject
    {
        [SerializeField]
        public List<GameEventListenerAudioClip> eventListeners = new List<GameEventListenerAudioClip> ();

        public void Raise (AudioClip arg)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised (arg);
        }

        public virtual void RegisterListener (GameEventListenerAudioClip listener)
        {
            if (!eventListeners.Contains (listener))
                eventListeners.Add (listener);
        }

        public virtual void UnRegisterListener (GameEventListenerAudioClip listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
#if UNITY_EDITOR
        [ContextMenu ("Show path")] public void ShowPath () { Debug.Log (UnityEditor.AssetDatabase.GetAssetPath (this)); }
#endif
    }

}
