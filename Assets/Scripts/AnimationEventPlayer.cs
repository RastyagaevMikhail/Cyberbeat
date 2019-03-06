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
        [SerializeField] StringUnityEventSelector selector;
        [SerializeField] string regexExpretions = string.Empty;
        Dictionary<float, string> animationTimeSlector = new Dictionary<float, string> ();

        public void OnBitGenerator (IBitData bitData)
        {
            string key = "";
            if (regexExpretions != string.Empty)
                key = Regex.Match (bitData.StringValue, regexExpretions).Value;
            else
                key = bitData.StringValue;

            if (selector.ContainsKey (key))
                animationTimeSlector.Add (bitData.StartTime, key);
        }

        public void OnBitPlayer (IBitData bitData)
        {
            float startTime = bitData.StartTime;

            if (animationTimeSlector.ContainsKey (startTime))
            {
                string key = animationTimeSlector[startTime];

                selector[key].Invoke();
                
                animationTimeSlector.Remove (startTime);
            }
        }
    }
}
