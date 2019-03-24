using GameCore;

using System;

using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (
        fileName = "AnimatorHashPlayer",
        menuName = "GameCore/Variable/AnimatorHashPlayer")]
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
            Value.Play (stateName);
        }
        public void Rebind ()
        {
            Value.Rebind ();
        }
        public int AnimationsCount { get { return Value.AnimationsCount; } }
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
            Value.PlayRandom();
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
