using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
    public abstract class TimeController : MonoBehaviour
    {
        [SerializeField]
        float time;
        public abstract IEnumerable<ITimeUpdateable> TimeUpdateables { get; }
        public bool StartCountTime { get; set; }

        void Awake ()
        {
            foreach (var item in TimeUpdateables)
                item.Start ();
        }
        void Update ()
        {
            if (!StartCountTime) return;

            foreach (var item in TimeUpdateables)
                item.UpdateInTime (time);
            time += Time.deltaTime;
        }
    }
}
