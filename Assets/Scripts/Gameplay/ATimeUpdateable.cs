using GameCore;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public abstract class ATimeUpdateable<TSavableVariable, TValue, TUnityEvent, TEventArgument>:
        ScriptableObject,
        ITimeUpdateableGeneric<TSavableVariable, TValue, TUnityEvent, TEventArgument>
        where TSavableVariable : SavableVariable<TValue>
        where TUnityEvent : UnityEvent<TEventArgument>
        {
            [SerializeField] protected TSavableVariable variable;
            public TSavableVariable Variable => variable;

            [SerializeField] TUnityEvent unityEvent;
            public TUnityEvent UnityEvent => unityEvent;

            public abstract ITimeItem CurrentTimeItem { get; set; }
            protected int indexOfTime;
            public bool TimesIsOver => indexOfTime >= TimeItems.Count ();

            public abstract IEnumerable<ITimeItem> TimeItems { get; }

            public virtual bool Start ()
            {
                if (TimesIsOver)
                {
                    OnTimeIsOver ();
                    return false;
                }
                CurrentTimeItem = TimeItems.First ();

                return true;
            }

            protected virtual void OnTimeIsOver ()
            {
                Debug.Log ($"{("TimesIsOver".err())} {this.ToString().warn()}", this);
            }

            public abstract void UpdateInTime (float time);
        }
}
