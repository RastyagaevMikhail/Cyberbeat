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

        [ContextMenu ("Toggle Savable")]
        void ToggleSavable () { isSavable = !isSavable; CheckSavable (); }

        [ContextMenu ("Check Savable")]
        void CheckSavable () { Debug.LogFormat ("{0} isSavable = {1}", name, isSavable); }

        public static implicit operator InputControllerComponent (InputControllerComponentVariable variable)
        {
            return variable.Value;
        }

        public void MoveRight ()
        {
            ValueFast.MoveRight ();
        } 

        public void MoveLeft ()
        {
            ValueFast.MoveLeft ();
        }
        public void SetControl (InputControlType controlTypeToSwitch)
        {
            ValueFast.SetControl(controlTypeToSwitch);
        }
    }
}
