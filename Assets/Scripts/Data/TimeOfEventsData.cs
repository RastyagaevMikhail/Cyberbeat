using SonicBloom.Koreo;

namespace CyberBeat
{
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    using System.Collections.Generic;

    using UnityEngine;

    public class TimeOfEventsData : SerializedScriptableObject
    {
        [OdinSerialize]
        [ShowInInspector] public List<TimeOfEvent> Times { get; private set; }

        public TimePointsData PointsData;
        [ShowInInspector] public float SampleRate { get; private set; }
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
    }

}
