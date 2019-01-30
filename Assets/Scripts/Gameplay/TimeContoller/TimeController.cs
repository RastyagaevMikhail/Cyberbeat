using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public class TimeController : MonoBehaviour
    {
        [SerializeField] float time;
        [SerializeField] UnityEvent OnAwake;
        [SerializeField] UnityEventFloat OnUpdate;
        // public abstract IEnumerable<ITimeUpdateable> TimeUpdateables { get; }
        public bool StartCountTime { get; set; }

        void Awake ()
        {
            // foreach (var item in TimeUpdateables)
            //     item.Start ();
            OnAwake.Invoke ();
        }
        void Update ()
        {
            if (!StartCountTime) return;

            // foreach (var item in TimeUpdateables)
            // {
            //     item.UpdateInTime (time);
            // }
            OnUpdate.Invoke (time);
            time += Time.deltaTime;
        }
    }
}
