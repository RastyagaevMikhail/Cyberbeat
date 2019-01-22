using GameCore;

using UnityEngine;
using UnityEngine.Events;
namespace FluffyUnderware.Curvy
{
[CreateAssetMenu(
    fileName = "CurvySplineSegmentRuntimeSet.asset",
    menuName = "FluffyUnderware.Curvy/RuntimeSet/CurvySplineSegment")]
    public class CurvySplineSegmentRuntimeSet : RuntimeSet<CurvySplineSegment> 
    {
        [SerializeField] UnityEventCurvySplineSegment onAddComplete;
        protected override UnityEvent<CurvySplineSegment> OnAddComplete
        {
            get
            {
                return onRemoveComplete;
            }
        }
        [SerializeField] UnityEventCurvySplineSegment onRemoveComplete;
        protected override UnityEvent<CurvySplineSegment> OnRemoveComplete
        {
            get
            {
                return onRemoveComplete;
            }
        }
     }
}
