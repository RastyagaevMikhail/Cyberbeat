using SonicBloom.Koreo;

namespace CyberBeat
{

    using GameCore;

    using System.Collections.Generic;

    using UnityEngine;

    public class TimeOfEventsData : ScriptableObject
    {
        public List<TimeOfEvent> Times;
        public TimePointsData PointsData;
        public float SampleRate;
        public void Init (float sampleRate, List<KoreographyEvent> events, TimePointsData pointsData = null)
        {
            SampleRate = sampleRate;

            Times = new List<TimeOfEvent> ();

            foreach (var e in events)
            {
                Times.Add (new TimeOfEvent (e, sampleRate));
            }
            if (pointsData)
                PointsData = pointsData;
                
            this.Save ();
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
