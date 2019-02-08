using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace FluffyUnderware.Curvy
{
    public class ConnectionCreator : MonoBehaviour
    {
        [SerializeField] CurvySplineSegmentVariable firstPoint;
        [SerializeField] CurvySplineSegmentVariable secondPoint;
        void Start ()
        {
            firstPoint.Value.ConnectTo (secondPoint.Value);
            secondPoint.Value.ConnectTo (firstPoint.Value); 
        }
    }
}
