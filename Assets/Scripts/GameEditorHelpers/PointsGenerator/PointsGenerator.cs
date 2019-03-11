using FluffyUnderware.Curvy.Controllers;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class PointsGenerator : MonoBehaviour
    {
        [SerializeField] SplineControllerVariable splineControllerVariable;
        [SerializeField] List<TimePoint> timePoints;
        SplineController spline => splineControllerVariable.Value;
        public void OnBit (IBitData bitData)
        {
            float tf = spline.AbsolutePosition / spline.Length;
            timePoints.Add (new TimePoint (tf, bitData.StringValue));
        }
        // [SerializeField] 


    }

    [System.Serializable]
    public class TimePoint
    {
        [SerializeField] float tf;
        [SerializeField] string lineName;

        public TimePoint (float tf, string lineName)
        {
            this.tf = tf;
            this.lineName = lineName;
        }
    }
}
