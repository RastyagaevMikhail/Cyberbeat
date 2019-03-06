using GameCore;
using UnityEngine;
namespace  FluffyUnderware.Curvy
{
    [CreateAssetMenu (
        fileName = "CurvySplineSegment", 
    menuName = "Variables/FluffyUnderware.Curvy/CurvySplineSegment")]
    public class CurvySplineSegmentVariable : SavableVariable<CurvySplineSegment>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (CurvySplineSegment value)
        {
            Value = value;
        }

        public void SetValue (CurvySplineSegmentVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From CurvySplineSegmentVariable
        }

        public override void LoadValue ()
        {
          // TODO: Load Code This From CurvySplineSegmentVariable
        }

       
        
        public static implicit operator CurvySplineSegment (CurvySplineSegmentVariable variable)
        {
            return variable.Value;
        }
    }
}

