using GameCore;

using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/ATimeUpdateable/TimeEventsItem")]
    public class TimeEventsItem : ATimeUpdateable<TimeOfEventsDataVariable, TimeOfEventsData, UnityEventTimeEvent, TimeEvent>
    {
        List<TimeOfEvent> Times { get { return Variable.ValueFast.Times; } }
        public override IEnumerable<ITimeItem> TimeItems => Variable.ValueFast.Times;

        public override ITimeItem CurrentTimeItem { get => currentTimeOfEvent; set => currentTimeOfEvent = value as TimeOfEvent; }
        TimeOfEvent currentTimeOfEvent;

        TimeEvent currentTimeEvent;
        bool isTime;
        bool lastTime;

        public override void Start ()
        {
            base.Start ();

            currentTimeEvent = new TimeEvent (isTime, currentTimeOfEvent);

        }

        public override void UpdateInTime (float time)
        {
            if (TimesIsOver) return;

            if (lastTime != isTime)
            {
                lastTime = isTime;

                UnityEvent.Invoke (currentTimeEvent.Init (isTime, currentTimeOfEvent));
                
                if (debug) Debug.Log ($"{UnityEvent.Log()}");
            }
            if (currentTimeOfEvent.Start <= time)
            {
                isTime = true;
                if (currentTimeOfEvent.End <= time)
                {
                    isTime = false;

                    indexOfTime++;
                    if (TimesIsOver) return;
                    CurrentTimeItem = Times[indexOfTime];
                }
            }
        }
        public override string ToString ()
        {
            return $"{Variable}";
        }
    }
}
