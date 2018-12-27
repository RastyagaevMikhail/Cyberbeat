using SonicBloom.Koreo;

namespace CyberBeat
{

    using System.Collections.Generic;

    using UnityEngine;

    public class TimeOfEventsData : ScriptableObject
    {
        public List<TimeOfEvent> Times { get; private set; }

        public TimePointsData PointsData;
        public float SampleRate { get; private set; }
        public void Init (float sampleRate, List<KoreographyEvent> events, TimePointsData pointsData)
        {
            SampleRate = sampleRate;

            Times = new List<TimeOfEvent> ();

            foreach (var e in events)
            {
                Times.Add (new TimeOfEvent (e, sampleRate));

            }
            PointsData = pointsData;
        }

        public List<TimeOfEvent> this [string payload]
        {
            get
            {
                return Times.FindAll (t => t.payload == payload);
            }
            set
            {
                var filterd = Times.FindAll (t => t.payload == payload);
                for (int i = 0; i < filterd.Count; i++)
                    filterd[i] = value[i];
            }
        }
    }

}
