using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{

    [CreateAssetMenu (fileName = "GameEvent", menuName = "Events/GameCore/GameEvent/void")]
    public class GameEvent : ScriptableObject
    {
        [SerializeField]
        public List<GameEventListener> eventListeners = new List<GameEventListener> ();

        public void Raise ()
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised ();
        }

        public virtual void RegisterListener (GameEventListener listener)
        {
            if (!eventListeners.Contains (listener))
                eventListeners.Add (listener);
        }

        public virtual void UnRegisterListener (GameEventListener listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
#if UNITY_EDITOR
        [ContextMenu ("Show path")] public void ShowPath () { Debug.Log (UnityEditor.AssetDatabase.GetAssetPath (this)); }
#endif
    }

}
