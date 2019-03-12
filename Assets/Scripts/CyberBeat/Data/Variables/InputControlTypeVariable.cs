
using GameCore;
using UnityEngine;
namespace  CyberBeat
{
    [CreateAssetMenu (
        fileName = "InputControlType", 
    menuName = "Variables/CyberBeat/InputControlType")]
    public class InputControlTypeVariable : SavableVariable<InputControlType>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }

        public void SetValue (InputControlTypeVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From InputControlTypeVariable
        }

        public override void LoadValue ()
        {
          // TODO: Load Code This From InputControlTypeVariable
        }        
        public static implicit operator InputControlType (InputControlTypeVariable variable)
        {
            return variable.Value;
        }
    }
}

