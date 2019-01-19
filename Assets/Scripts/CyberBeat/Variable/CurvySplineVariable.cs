using FluffyUnderware.Curvy;

using GameCore;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (fileName = "CurvySpline", menuName = "Variables/CyberBeat/CurvySpline")]
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

        [ContextMenu ("Toggle Savable")]
        void ToggleSavable () { isSavable = !isSavable; CheckSavable (); }

        [ContextMenu ("Check Savable")]
        void CheckSavable () { Debug.LogFormat ("{0} isSavable = {1}", name, isSavable); }

        public static implicit operator CurvySpline (CurvySplineVariable variable)
        {
            return variable.Value;
        }
    }
}
