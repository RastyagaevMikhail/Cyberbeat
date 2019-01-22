using FluffyUnderware.Curvy;

using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    public class SplinePointsGenerator : MonoBehaviour
    {
        private CurvySpline _spline = null;
        public CurvySpline spline { get { if (_spline == null) _spline = GetComponent<CurvySpline> (); return _spline; } }

        [ContextMenu ("AddPoint")]
        void AddPoint ()
        {
            CurvySplineSegment lastVisibleControlPoint = spline.LastVisibleControlPoint;
            Vector3 newPos = lastVisibleControlPoint.position;
            const float distance = 500f;
            var x = Tools.RandomOne * Tools.RandomZero;
            var y = Tools.RandomOne * Tools.RandomZero;
            var z = Tools.RandomOne * Tools.RandomZero;
            Vector3 offset = (new Vector3 (x, y, z) * distance);
            offset -= (lastVisibleControlPoint.transform.forward * distance);
            newPos = newPos + offset;
            var segments = spline.Add (newPos);
            var newSegment = segments.First ();
            newSegment.AutoHandles = false;
            newSegment.HandleIn = lastVisibleControlPoint.HandleIn - offset;
        }
    }
}
