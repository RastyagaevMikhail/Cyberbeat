using GameCore;
using SonicBloom.Koreo;
using UnityEngine;
namespace  CyberBeat
{
    [CreateAssetMenu (
        fileName = "Koreography", 
    menuName = "Variables/CyberBeat/Koreography")]
    public class KoreographyVariable : SavableVariable<Koreography>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (Koreography value)
        {
            Value = value;
        }

        public void SetValue (KoreographyVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From KoreographyVariable
        }

        public override void LoadValue ()
        {
          // TODO: Load Code This From KoreographyVariable
        }

        [ContextMenu ("Toggle Savable")]
        void ToggleSavable () { isSavable = !isSavable; CheckSavable (); }

        [ContextMenu ("Check Savable")]
        void CheckSavable () { Debug.LogFormat ("{0} isSavable = {1}", name, isSavable); }
        
        public static implicit operator Koreography (KoreographyVariable variable)
        {
            return variable.Value;
        }
    }
}

