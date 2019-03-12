using GameCore;

using System;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (
        fileName = "InputControllerComponent",
        menuName = "Variables/CyberBeat/InputControllerComponent")]
    public class InputControllerComponentVariable : SavableVariable<InputControllerComponent>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (InputControllerComponent value)
        {
            Value = value;
        }

        public void SetValue (InputControllerComponentVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From InputControllerComponentVariable
        }

        public override void LoadValue ()
        {
            // TODO: Load Code This From InputControllerComponentVariable
        }

       

        public static implicit operator InputControllerComponent (InputControllerComponentVariable variable)
        {
            return variable.Value;
        }

        public void TapRight ()
        {
            Value.TapRight ();
        } 

        public void TapLeft ()
        {
            Value.TapLeft ();
        }
        public void SetControl (InputControlType controlTypeToSwitch)
        {
            Value.SetControl(controlTypeToSwitch);
        }
    }
}
