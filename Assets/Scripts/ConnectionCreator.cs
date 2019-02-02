using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace FluffyUnderware.Curvy
{
    public class ConnectionCreator : MonoBehaviour
    {
        [SerializeField] CurvySplineSegmentRuntimeSet segmentsForConntetionSet;
        [SerializeField] CurvySplineSegmentVariable firstPoint;
        [SerializeField] CurvySplineSegmentVariable secondPoint;
        void Start ()
        {
            firstPoint.Value.ConnectTo (secondPoint.Value);
            secondPoint.Value.ConnectTo (firstPoint.Value);
            // ConnectPointsInSet();
        }

        private void ConnectPointsInSet ()
        {
            CurvySplineSegment[] controlPoints = segmentsForConntetionSet.ToArray ();
            var connection = CurvyConnection.Create (controlPoints);
            connection.AutoSetFollowUp ();
            connection.ControlPoints.ForEach (cp => cp.ConnectionSyncRotation = cp.ConnectionSyncPosition = true);
        }
    }
}
