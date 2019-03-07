using FluffyUnderware.Curvy.Controllers;

using GameCore;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (
        fileName = "SplineController",
        menuName = "Variables/CyberBeat/SplineController")]
    public class SplineControllerVariable : SavableVariable<SplineController>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (SplineController value)
        {
            Value = value;
        }

        public void SetValue (SplineControllerVariable value)
        {
            Value = value.Value;
        }
        public float Speed { set => ValueFast.Speed = value; }
        public override void SaveValue ()
        {
            // TODO: Save Code This From SplineControllerVariable
        }

        public override void LoadValue ()
        {
            // TODO: Load Code This From SplineControllerVariable
        }

       

        public static implicit operator SplineController (SplineControllerVariable variable)
        {
            return variable.Value;
        }
    }
}
