using GameCore;

using Sirenix.OdinInspector;

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
        Queue<string> presetsQueue = new Queue<string> ();
        [SerializeField] bool debug;
        public void OnBitGeneratorPresetChosed (string choosedPresetName)
        {
            if (debug) Debug.Log (choosedPresetName, this);
            presetsQueue.Enqueue (choosedPresetName);
        }

        public void OnBitPlayer (IBitData bitData)
        {
            if (debug)
                Debug.Log (presetsQueue.Log (), this);
            if (presetsQueue.Count == 0) return;
            string key = presetsQueue.Dequeue ();
            if (debug) Debug.Log (key, this);

            selector[key]?.Invoke ();

        }
    }
}
