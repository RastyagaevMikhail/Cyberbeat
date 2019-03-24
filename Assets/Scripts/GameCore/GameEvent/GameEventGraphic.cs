using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{

    [CreateAssetMenu (fileName = "GameEventGraphic", menuName = "GameCore/GameEvent/UnityEngine/Graphic")]
    public class GameEventGraphic : ScriptableObject
    {
        [SerializeField]
        public List<GameEventListenerGraphic> eventListeners = new List<GameEventListenerGraphic> ();

        public void Raise (Graphic arg)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised (arg);
        }

        public virtual void RegisterListener (GameEventListenerGraphic listener)
        {
            if (!eventListeners.Contains (listener))
                eventListeners.Add (listener);
        }

        public virtual void UnRegisterListener (GameEventListenerGraphic listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
#if UNITY_EDITOR
        [ContextMenu ("Show path")] public void ShowPath () { Debug.Log (UnityEditor.AssetDatabase.GetAssetPath (this)); }
#endif
    }

}
