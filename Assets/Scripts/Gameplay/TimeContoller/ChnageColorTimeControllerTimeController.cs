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
            IBitData bit = bits.Dequeue();
            if(bit.StartTime != bitTime)
            {
                OnBeatDeath (bitTime);
                return;
            }
            var randomSplitedString = bit.StringValue.Split('/');
            if (randomSplitedString.Length > 1)
            {
                pool.Pop (randomSplitedString[1]);
            }
        }
    }
}
