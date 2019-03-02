using GameCore;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public abstract class ATimeUpdateable : ScriptableObject
    {
        public abstract void Start ();
        public abstract void OnTimeIsOver ();
        public abstract void UpdateInTime (float time);
    }
    public abstract class ATimeUpdateable<TSavableVariable, TValue, TUnityEvent, TEventArgument> : ATimeUpdateable
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
        public bool TimesIsOver => indexOfTime >= TimeItems.Count () || TimeItems.Count () == 0;

        public abstract IEnumerable<ITimeItem> TimeItems { get; }

        public override void Start ()
        {
            indexOfTime = 0;
            if (TimesIsOver)
            {
                OnTimeIsOver ();
                return;
            }
            CurrentTimeItem = TimeItems.First ();
        }

        public override void OnTimeIsOver ()
        {
            if (debug) Debug.Log ($"{("TimesIsOver".err())} {this.ToString().warn()}", this);
        }

        public override abstract void UpdateInTime (float time);
    }
}
