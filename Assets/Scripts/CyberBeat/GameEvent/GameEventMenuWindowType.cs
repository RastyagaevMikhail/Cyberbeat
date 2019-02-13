using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{

    [CreateAssetMenu (
        fileName = "GameEventMenuWindowType", 
        menuName = "CyberBeat/GameEvent/MenuWindowType")]
    public class GameEventMenuWindowType : ScriptableObject
    {
        [SerializeField]
        public List<GameEventListenerMenuWindowType> eventListeners = new List<GameEventListenerMenuWindowType> ();

        public void Raise (MenuWindowType arg)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised (arg);
        }

        public virtual void RegisterListener (GameEventListenerMenuWindowType listener)
        {
            if (!eventListeners.Contains (listener))
                eventListeners.Add (listener);
        }

        public virtual void UnRegisterListener (GameEventListenerMenuWindowType listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
#if UNITY_EDITOR
        [ContextMenu ("Show path")] public void ShowPath () { Debug.Log (UnityEditor.AssetDatabase.GetAssetPath (this)); }
#endif
    }

}
