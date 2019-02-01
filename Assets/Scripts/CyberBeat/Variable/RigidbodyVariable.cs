using GameCore;
using UnityEngine;
namespace  CyberBeat
{
    [CreateAssetMenu (
        fileName = "Rigidbody", 
    menuName = "Variables/CyberBeat/Rigidbody")]
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

        [ContextMenu ("Toggle Savable")]
        void ToggleSavable () { isSavable = !isSavable; CheckSavable (); }

        [ContextMenu ("Check Savable")]
        void CheckSavable () { Debug.LogFormat ("{0} isSavable = {1}", name, isSavable); }
        
        public static implicit operator Rigidbody (RigidbodyVariable variable)
        {
            return variable.Value;
        }
    }
}

