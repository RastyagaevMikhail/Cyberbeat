using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{

    [RequireComponent (typeof (GameEventListener))]
    public class TimeEventsController : MonoBehaviour
    {
        [SerializeField] TimeOfEventsData dataTime;
        [SerializeField] bool enableFilter;
        [SerializeField] string payloadFilter = "Combo";
        List<TimeOfEvent> Times { get { return enableFilter ? dataTime[payloadFilter] : dataTime.Times; } }
        TimeOfEvent currentTime;

        private void Start ()
        {
            currentTime = Times.First ();
        }

        bool isTime;
        bool lastTime;
        private float time;
        //* Set On GameEventListener, When Need start Write Times
        public bool _startCountTime { get; set; }
        private int indexOfTime;

        [SerializeField] TimeEventVariable timeEvent;

        void Update ()
        {
            if (!_startCountTime) return;

            time += Time.deltaTime;
            if (lastTime != isTime)
            {
                lastTime = isTime;
                if (timeEvent != null) timeEvent.SetValue (new TimeEvent (isTime, currentTime));
            }
            if (currentTime.Start <= time)
            {
                isTime = true;
                if (currentTime.End <= time)
                {
                    isTime = false;

                    indexOfTime++;
                    if (indexOfTime >= Times.Count) return;
                    currentTime = Times[indexOfTime];
                }
            }
        }

        public void SetFilterEnabled(bool Value) {
            enableFilter = Value;
        }
    }
}
