using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class ChnageColorTimeControllerTimeController : MonoBehaviour
    {
        [SerializeField] PoolVariable pool;
        Queue<IBitData> bits = new Queue<IBitData> ();
        public void OnGeneratorBit (IBitData bitData)
        {
            bits.Enqueue (bitData);
        }
        public void OnBeatDeath (float bitTime)
        {
            if (bits.Count == 0) return;
            IBitData bit = bits.Dequeue ();

            if (bit.StartTime == bitTime)
            {
                pool.Pop (bit.StringValue);
            }
            else
            {
                while (bits.Count > 0 && bit.StartTime != bitTime) { bit = bits.Dequeue (); }
                pool.Pop (bit.StringValue);
            }
        }
    }
}
