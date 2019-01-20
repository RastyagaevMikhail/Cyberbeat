using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/ATimeUpdateable/TimeEventsItem")]
    public class TimeEventsItem : ATimeUpdateable<TimeOfEventsDataVariable, TimeOfEventsData, UnityEventTimeEvent, TimeEvent>
    {
        List<TimeOfEvent> Times { get { return Variable.Value.Times; } }
        public override IEnumerable<ITimeItem> TimeItems => Variable.Value.Times;

        public override ITimeItem CurrentTimeItem { get => currentTimeOfEvent; set => currentTimeOfEvent = value as TimeOfEvent; }
        TimeOfEvent currentTimeOfEvent;

        TimeEvent currentTimeEvent;
        bool isTime;
        bool lastTime;

        public override bool Start ()
        {
            bool baseStartResult = base.Start ();

            currentTimeEvent = new TimeEvent (isTime, currentTimeOfEvent);

            return baseStartResult;
        }

        public override void UpdateInTime (float time)
        {
            if (TimesIsOver) return;

            if (lastTime != isTime)
            {
                lastTime = isTime;

                UnityEvent.Invoke (currentTimeEvent.Init (isTime, currentTimeOfEvent));
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
