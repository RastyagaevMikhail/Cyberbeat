using GameCore;

using SonicBloom.Koreo;

using System;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (
        fileName = "Koreographer",
        menuName = "Variables/CyberBeat/Koreographer")]
    public class KoreographerVariable : SavableVariable<Koreographer>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }

        public void RegisterForEvents (string eventID, KoreographyEventCallback callback)
        {
            if (Value)
                Value.RegisterForEvents (eventID, callback);
        }

        public void SetValue (Koreographer value)
        {
            Value = value;
        }

        public void SetValue (KoreographerVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From KoreographerVariable
        }

        public override void LoadValue ()
        {
            // TODO: Load Code This From KoreographerVariable
        }

        [ContextMenu ("Toggle Savable")]
        void ToggleSavable () { isSavable = !isSavable; CheckSavable (); }

        [ContextMenu ("Check Savable")]
        void CheckSavable () { Debug.LogFormat ("{0} isSavable = {1}", name, isSavable); }

        public static implicit operator Koreographer (KoreographerVariable variable)
        {
            return variable.Value;
        }
    }
}
