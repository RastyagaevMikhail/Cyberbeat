using System;

namespace CyberBeat
{
    public struct TimeEvent
    {
        public bool isTime;
        public TimeOfEvent timeOfEvent;
        public TimeEvent (bool _isTime, TimeOfEvent _timeOfevent)
        {
            isTime = _isTime;
			timeOfEvent = _timeOfevent ;
        }

        public TimeEvent Init(bool isTime, TimeOfEvent timeOfEvent)
        {
            this.isTime = isTime;
            this.timeOfEvent = timeOfEvent;
            return this;
        }
    }
}