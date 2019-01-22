using System.Collections.Generic;

using UnityEngine;
namespace FluffyUnderware.Curvy
{

    [CreateAssetMenu (
        fileName = "GameEventCurvySpline", 
        menuName = "FluffyUnderware.Curvy/GameEvent/CurvySpline")]
    public class GameEventCurvySpline : ScriptableObject
    {
        [SerializeField]
        public List<GameEventListenerCurvySpline> eventListeners = new List<GameEventListenerCurvySpline> ();

        public void Raise (CurvySpline arg)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised (arg);
        }

        public virtual void RegisterListener (GameEventListenerCurvySpline listener)
        {
            if (!eventListeners.Contains (listener))
                eventListeners.Add (listener);
        }

        public virtual void UnRegisterListener (GameEventListenerCurvySpline listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
#if UNITY_EDITOR
        [ContextMenu ("Show path")] public void ShowPath () { Debug.Log (UnityEditor.AssetDatabase.GetAssetPath (this)); }
#endif
    }

}
