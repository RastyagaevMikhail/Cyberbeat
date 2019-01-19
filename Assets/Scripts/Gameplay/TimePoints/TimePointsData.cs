using System.Collections.Generic;

namespace CyberBeat
{

    using UnityEngine;

    [CreateAssetMenu (fileName = "TimePointsData", menuName = "CyberBeat/TimePointsData", order = 0)]
    public class TimePointsData : ScriptableObject
    {
        [SerializeField]
        public List<TimePoints> points = new List<TimePoints> ();
        public List<TimePoints> this [string payload]
        {
            get
            {
                return points.FindAll (t => t.payload == payload);
            }
            set
            {
                var filterd = points.FindAll (t => t.payload == payload);
                for (int i = 0; i < filterd.Count; i++)
                    filterd[i] = value[i];
            }
        }
    }
}
