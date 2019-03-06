using DG.Tweening;

using GameCore;

using System;

using UnityEngine;
namespace GameCore.DoTween
{
    [CreateAssetMenu (
        fileName = "DoTweenGraphics",
        menuName = "GameCore/Variables/DoTween/Graphics")]
    public class DoTweenGraphicsVariable : SavableVariable<DoTweenGraphics>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }

        public void SetValue (DoTweenGraphicsVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From DoTweenGraphicsVariable
        }

        public override void LoadValue ()
        {
            // TODO: Load Code This From DoTweenGraphicsVariable
        }
        public static implicit operator DoTweenGraphics (DoTweenGraphicsVariable variable)
        {
            return variable.Value;
        }

        public void DoFade (float durationTime)
        {
            DoFadeTween (durationTime);
        }
        public Tween DoFadeTween (float durationTime)
        {
            return ValueFast.DoFadeTween (durationTime);
        }
        public void DoFadeIn (float durationTime)
        {
            DoFadeInTween (durationTime);
        }
        public Tween DoFadeInTween (float durationTime)
        {
            return ValueFast.DoFadeInTween (durationTime);
        }
        public void DoFadeOut (float durationTime)
        {
            DoFadeOutTween (durationTime);
        }
        public Tween DoFadeOutTween (float durationTime)
        {
            return ValueFast.DoFadeOutTween (durationTime);
        }
    }
}
