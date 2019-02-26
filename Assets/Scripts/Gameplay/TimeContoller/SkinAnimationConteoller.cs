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
        List<IBitData> bits = new List<IBitData> ();
        public void OnGeneratorBit (IBitData bitData)
        {
            bits.Add (bitData);
        }
        public void OnBeatDeath (float bitTime)
        {
            if (debug)
                Debug.LogFormat ("bits.Count = {0}", bits.Count);
            if (bits.Count == 0) return;

            if (debug)
            {
                Debug.LogFormat ("bitTime = {0}", bitTime);
            }
            var bitsList = new List<IBitData> ();
            foreach (var bit in bits)
            {
                if (bit.StartTime == bitTime)
                {
                    animator.Rebind ();
                    animator.PlayRandom ();
                    break;
                }
                else
                {
                    bitsList.Add (bit);
                }
            }
            foreach (var bit in bitsList)
            {
                if (bit.StartTime <= bitTime)
                {
                    if(bits.Contains(bit))
                    {
                        bits.Remove(bit);
                    }
                }
            }

        }
    }
}
