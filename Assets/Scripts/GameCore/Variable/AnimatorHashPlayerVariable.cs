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
        public int AnimationsCount { get { return ValueFast.AnimationsCount; } }
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

        public void PlayRandom()
        {
            ValueFast.PlayRandom();
        }

        public override void LoadValue ()
        {
            // TODO: Load Code This From AnimatorHashPlayerVariable
        }

       

        public static implicit operator AnimatorHashPlayer (AnimatorHashPlayerVariable variable)
        {
            return variable.Value;
        }
    }
}
