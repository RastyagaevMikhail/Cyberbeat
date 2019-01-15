using System.Collections.Generic;

namespace CyberBeat
{
    using System.Linq;

    using UnityEngine;

    [CreateAssetMenu (fileName = "TimePointsData", menuName = "CyberBeat/TimePointsData", order = 0)]
    public class TimePointsData : ScriptableObject
    {
        [SerializeField]
        public List<TimePoints> points = new List<TimePoints> ();

        Dictionary<string, List<TimePoints>> _filterd = null;
        Dictionary<string, List<TimePoints>> filterd
        {
            get
            {
                return _filterd ?? (_filterd = points.ToDictionary (p => p.payload, p => points.FindAll (pts => pts.payload == p.payload)));
            }
        }

        public List<TimePoints> this [string payload]
        {
            get
            {
                List<TimePoints> timePoints = null;
                filterd.TryGetValue (payload, out timePoints);
                return timePoints;
            }
        }
    }
}
