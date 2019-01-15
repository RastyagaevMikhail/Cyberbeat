using SonicBloom.Koreo;

namespace CyberBeat
{

    using GameCore;

    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    using UnityEngine;

    public class TimeOfEventsData : ScriptableObject
    {
        public List<TimeOfEvent> Times;
        public TimePointsData PointsData;
        public void Init (float sampleRate, List<KoreographyEvent> events, TimePointsData pointsData = null)
        {

            Times = new List<TimeOfEvent> ();

            foreach (var e in events)
            {
                Times.Add (new TimeOfEvent (e, sampleRate));
            }
            if (pointsData)
                PointsData = pointsData;

            this.Save ();
        }
        Dictionary<string, List<TimeOfEvent>> _filterd = null;
        Dictionary<string, List<TimeOfEvent>> filterd
        {
            get
            {
                return _filterd ?? (_filterd = Times.ToDictionary (t => t.payload, t =>Times.FindAll (ti => ti.payload == t.payload)));
            }
        }

        public List<TimeOfEvent> this [string payload]
        {
            get
            {
                List<TimeOfEvent> timeOfEvents = null;
                filterd.TryGetValue (payload, out timeOfEvents);
                return timeOfEvents;
            }
        }
    }

}
