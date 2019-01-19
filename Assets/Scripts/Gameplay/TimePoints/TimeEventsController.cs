using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    public class TimeEventsController : MonoBehaviour
    {
        [SerializeField]
        List<TimeEventsItem> timeEventsItems;
        [SerializeField]
        float time;
        public bool StartCountTime { get; set; }
        void Awake ()
        {
            foreach (var item in timeEventsItems)
                item.Start ();
        }
        void Update ()
        {
            if (!StartCountTime) return;

            time += Time.deltaTime;
            foreach (var item in timeEventsItems)
                item.UpdateInTime (time);
        }
    }

    [Serializable]
    public class TimeEventsItem
    {
        [SerializeField] TimeOfEventsDataVariable dataTimeVariable;
        [SerializeField] UnityEventTimeEvent OnTimneEventChanged;
        TimeOfEventsData dataTime { get { return dataTimeVariable.Value; } }
        List<TimeOfEvent> Times { get { return dataTime.Times; } }
        private bool TimesIsOver => indexOfTime >= Times.Count;
        TimeOfEvent currentTime;
        TimeEvent currentTimeEvent;
        int indexOfTime;
        bool isTime;
        bool lastTime;

        public bool Start ()
        {
            if (TimesIsOver)
            {
                Debug.Log ($"TimesIsOver {this}");
                return false;
            }
            currentTime = Times.First ();
            currentTimeEvent = new TimeEvent (isTime, currentTime);
            return true;
        }
        public void UpdateInTime (float time)
        {
            if (TimesIsOver) return;

            if (lastTime != isTime)
            {
                lastTime = isTime;

                OnTimneEventChanged.Invoke (currentTimeEvent.Init (isTime, currentTime));
            }
            if (currentTime.Start <= time)
            {
                isTime = true;
                if (currentTime.End <= time)
                {
                    isTime = false;

                    indexOfTime++;
                    if (TimesIsOver) return;
                    currentTime = Times[indexOfTime];
                }
            }
        }
        public override string ToString ()
        {
            return $"{dataTimeVariable}" + base.ToString ();
        }

    }
}
