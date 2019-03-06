using GameCore;

using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (fileName = "Rigidbody", menuName = "GameCore/Variables/Rigidbody")]
    public class RigidbodyVariable : SavableVariable<Rigidbody>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (Rigidbody value)
        {
            Value = value;
        }

        public void SetValue (RigidbodyVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From RigidbodyVariable
        }

        public override void LoadValue ()
        {
            // TODO: Load Code This From RigidbodyVariable
        }

       

        public static implicit operator Rigidbody (RigidbodyVariable variable)
        {
            return variable.Value;
        }
    }
}
