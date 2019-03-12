using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
    public class OnTimeBeforeController : MonoBehaviour
    {
        [SerializeField] float timeBefore = 0.2f;
        [SerializeField] UnityEventIBitData onTimeBefore;
        Queue<IBitData> queueBits = new Queue<IBitData> ();
        public void OnGenertorBit (IBitData bitData)
        {
            queueBits.Enqueue (bitData);
        }

        public void OnPlayerTimeUpdate (float time)
        {
            if (queueBits.Count == 0) return;

            float bitTime = queueBits.Peek ().StartTime;
            if (time  > bitTime - timeBefore)
            {
                onTimeBefore.Invoke(queueBits.Dequeue());
            }
        }
    }
}
