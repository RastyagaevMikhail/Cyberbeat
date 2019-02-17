using GameCore;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public abstract class ATimeUpdateable<TSavableVariable, TValue, TUnityEvent, TEventArgument> : ScriptableObject
    {
        [SerializeField] protected TSavableVariable variable;
        public TSavableVariable Variable
        {
            get => variable;
#if UNITY_EDITOR
            set => variable = value;
#endif
        }

        [SerializeField] TUnityEvent unityEvent;
        public TUnityEvent UnityEvent => unityEvent;
        [SerializeField] protected bool debug;
        public abstract ITimeItem CurrentTimeItem { get; set; }
        protected int indexOfTime;
        public bool TimesIsOver => indexOfTime >= TimeItems.Count ();

        public abstract IEnumerable<ITimeItem> TimeItems { get; }

        public virtual void Start ()
        {
            indexOfTime = 0;
            if (TimesIsOver)
            {
                OnTimeIsOver ();
                return;
            }
            CurrentTimeItem = TimeItems.First ();
        }

        protected virtual void OnTimeIsOver ()
        {
            if (debug) Debug.Log ($"{("TimesIsOver".err())} {this.ToString().warn()}", this);
        }

        public abstract void UpdateInTime (float time);
    }
}
