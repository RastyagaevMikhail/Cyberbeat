using GameCore;
using UnityEngine;
namespace  FluffyUnderware.Curvy
{
    [CreateAssetMenu (
        fileName = "CurvySpline", 
    menuName = "Variables/FluffyUnderware.Curvy/CurvySpline")]
    public class CurvySplineVariable : SavableVariable<CurvySpline>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (CurvySpline value)
        {
            Value = value;
        }

        public void SetValue (CurvySplineVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From CurvySplineVariable
        }

        public override void LoadValue ()
        {
          // TODO: Load Code This From CurvySplineVariable
        }

       
        
        public static implicit operator CurvySpline (CurvySplineVariable variable)
        {
            return variable.Value;
        }
    }
}

