using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{

    [CreateAssetMenu (
        fileName = "GameEventColorInterractorInfo", 
        menuName = "CyberBeat/GameEvent/ColorInterractorInfo")]
    public class GameEventColorInterractorInfo : ScriptableObject
    {
        [SerializeField]
        public List<GameEventListenerColorInterractorInfo> eventListeners = new List<GameEventListenerColorInterractorInfo> ();

        public void Raise (ColorInterractor.Info arg)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised (arg);
        }

        public virtual void RegisterListener (GameEventListenerColorInterractorInfo listener)
        {
            if (!eventListeners.Contains (listener))
                eventListeners.Add (listener);
        }

        public virtual void UnRegisterListener (GameEventListenerColorInterractorInfo listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
#if UNITY_EDITOR
        [ContextMenu ("Show path")] public void ShowPath () { Debug.Log (UnityEditor.AssetDatabase.GetAssetPath (this)); }
#endif
    }

}
