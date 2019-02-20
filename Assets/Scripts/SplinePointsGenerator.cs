using FluffyUnderware.Curvy;

using GameCore;

using Sirenix.OdinInspector;

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

        [SerializeField] float distance = 500f;
        int pointsCount = 18;

        void AddPoints ()
        {
            for (int i = 0; i < pointsCount; i++)
            {
                AddPointOnePointInEnd ();
            }
        }

        [Button]
        [ContextMenu ("Add One Point in End")]
        void AddPointOnePointInEnd ()
        {
            var lastVisibleControlPoint = spline.LastVisibleControlPoint ? spline.LastVisibleControlPoint.transform : spline.ControlPoints[0].transform;

            Vector3 newPos = lastVisibleControlPoint.position;

            bool horizontal = Tools.RandomBool;

            float dir = Tools.RandomDir;

            var vecDir = ((horizontal ? lastVisibleControlPoint.right : lastVisibleControlPoint.up)) * dir;

            Vector3 forward = lastVisibleControlPoint.forward;
            int multiplayer = (horizontal ? 2 : 1);
            newPos += ((vecDir + forward) * distance * multiplayer);

            var segments = spline.Add (newPos);
            var newSegment = segments.First ();

            newSegment.AutoHandles = false;
            newSegment.HandleIn = newSegment.transform.InverseTransformPoint (lastVisibleControlPoint.position + forward * distance * multiplayer);
            newSegment.HandleOut = Vector3.zero;
            newSegment.position = newPos;
            newSegment.transform.forward = vecDir;
        }
    }
}
