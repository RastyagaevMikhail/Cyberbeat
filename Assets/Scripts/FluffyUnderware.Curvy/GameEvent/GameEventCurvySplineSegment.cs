using System.Collections.Generic;

using UnityEngine;
namespace FluffyUnderware.Curvy
{

    [CreateAssetMenu (
        fileName = "GameEventCurvySplineSegment", 
        menuName = "FluffyUnderware.Curvy/GameEvent/CurvySplineSegment")]
    public class GameEventCurvySplineSegment : ScriptableObject
    {
        [SerializeField]
        public List<GameEventListenerCurvySplineSegment> eventListeners = new List<GameEventListenerCurvySplineSegment> ();

        public void Raise (CurvySplineSegment arg)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised (arg);
        }

        public virtual void RegisterListener (GameEventListenerCurvySplineSegment listener)
        {
            if (!eventListeners.Contains (listener))
                eventListeners.Add (listener);
        }

        public virtual void UnRegisterListener (GameEventListenerCurvySplineSegment listener)
        {
            if (eventListeners.Contains (listener))
                eventListeners.Remove (listener);
        }
#if UNITY_EDITOR
        [ContextMenu ("Show path")] public void ShowPath () { Debug.Log (UnityEditor.AssetDatabase.GetAssetPath (this)); }
#endif
    }

}
