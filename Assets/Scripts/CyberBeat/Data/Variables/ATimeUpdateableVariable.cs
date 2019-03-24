
using GameCore;
using UnityEngine;
namespace  CyberBeat
{
    [CreateAssetMenu (
        fileName = "ATimeUpdateable", 
    menuName = "CyberBeat/Variable/ATimeUpdateable")]
    public class ATimeUpdateableVariable : SavableVariable<ATimeUpdateable>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }

        public void SetValue (ATimeUpdateableVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From ATimeUpdateableVariable
        }

        public override void LoadValue ()
        {
          // TODO: Load Code This From ATimeUpdateableVariable
        }        
        public static implicit operator ATimeUpdateable (ATimeUpdateableVariable variable)
        {
            return variable.Value;
        }
    }
}

