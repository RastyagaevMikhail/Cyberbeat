using GameCore;

using System;

using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (
        fileName = "AnimatorHashPlayer",
        menuName = "Variables/GameCore/AnimatorHashPlayer")]
    public class AnimatorHashPlayerVariable : SavableVariable<AnimatorHashPlayer>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }

        public void Play (string stateName)
        {
            ValueFast.Play (stateName);
        }
        public void Rebind ()
        {
            ValueFast.Rebind ();
        }

        public void SetValue (AnimatorHashPlayer value)
        {
            Value = value;
        }

        public void SetValue (AnimatorHashPlayerVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From AnimatorHashPlayerVariable
        }

        public override void LoadValue ()
        {
            // TODO: Load Code This From AnimatorHashPlayerVariable
        }

        [ContextMenu ("Toggle Savable")]
        void ToggleSavable () { isSavable = !isSavable; CheckSavable (); }

        [ContextMenu ("Check Savable")]
        void CheckSavable () { Debug.LogFormat ("{0} isSavable = {1}", name, isSavable); }

        public static implicit operator AnimatorHashPlayer (AnimatorHashPlayerVariable variable)
        {
            return variable.Value;
        }
    }
}
