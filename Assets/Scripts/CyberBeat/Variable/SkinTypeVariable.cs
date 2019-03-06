using GameCore;
using UnityEngine;
namespace  CyberBeat
{
    [CreateAssetMenu (
        fileName = "SkinType", 
    menuName = "Variables/CyberBeat/SkinType")]
    public class SkinTypeVariable : SavableVariable<SkinType>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (SkinType value)
        {
            Value = value;
        }

        public void SetValue (SkinTypeVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From SkinTypeVariable
        }

        public override void LoadValue ()
        {
          // TODO: Load Code This From SkinTypeVariable
        }

       
        
        public static implicit operator SkinType (SkinTypeVariable variable)
        {
            return variable.Value;
        }
    }
}

