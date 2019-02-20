using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class SkinAnimationConteoller : MonoBehaviour
    {
        [SerializeField] AnimatorHashPlayerVariable animator;
        [SerializeField] bool debug;
        Queue<IBitData> bits = new Queue<IBitData> ();
        public void OnGeneratorBit (IBitData bitData)
        {
            bits.Enqueue (bitData);
        }
        public void OnBeatDeath (float bitTime)
        {
            if (debug)
                Debug.LogFormat ("bits.Count = {0}", bits.Count);
            if (bits.Count == 0) return;
            IBitData bit = bits.Dequeue ();
            if (debug)
            {
                Debug.LogFormat ("bitTime = {0}", bitTime);
                Debug.LogFormat ("bit = {0}", bit);
            }
            if (bit.StartTime != bitTime)
            {
                OnBeatDeath (bitTime);
                return;
            }
            animator.Rebind ();
            animator.PlayRandom();
        }
    }
}
