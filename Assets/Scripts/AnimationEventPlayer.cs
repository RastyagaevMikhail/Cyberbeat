using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

using UnityEngine;
namespace CyberBeat
{
    public class AnimationEventPlayer : MonoBehaviour
    {
        [SerializeField] StringDoTweenGraphicsVariableSelector AnimationSelector;
        [SerializeField] string regexExpretions = string.Empty;
        [SerializeField] float fadeTime = 0.2f;
        Dictionary<float, string> animationTimeSlector = new Dictionary<float, string> ();

        public void OnBitGenerator (IBitData bitData)
        {
            string key = "";
            if (regexExpretions != string.Empty)
                key = Regex.Match (bitData.StringValue, regexExpretions).Value;
            else
                key = bitData.StringValue;

            if (AnimationSelector.ContainsKey (key))
                animationTimeSlector.Add (bitData.StartTime, key);
        }

        public void OnBitPlayer (IBitData bitData)
        {
            float startTime = bitData.StartTime;

            if (animationTimeSlector.ContainsKey (startTime))
            {
                string key = animationTimeSlector[startTime];

                var doTweenGraphicsVariable = AnimationSelector[key];

                var fadeInTween = doTweenGraphicsVariable.DoFadeInTween (Time.deltaTime);

                fadeInTween.onComplete = () =>
                {
                    doTweenGraphicsVariable.DoFade (fadeTime);
                };
                animationTimeSlector.Remove (startTime);
            }
        }
    }
}
