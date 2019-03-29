using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using static CyberBeat.GameEventListenerIBitData;

namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/ATimeUpdateable/BitTimeItem/TrackBitItemData")]
    public class TrackBitItemData : ScriptableObject //BitTimeItem<TrackBit>
    {
        public TrackBitsCollectionVariable Variable;

        [SerializeField] UnityEventIBitData unityEvent;
        [SerializeField] protected bool debug;
        int indexOfTime;
        public bool TimesIsOver => indexOfTime >= Bits.Length || Bits.Length == 0;
        IBitData[] Bits => Variable.Value.Bits;
        IBitData currentBit;
        [SerializeField] UnityEventIBitData onStart;

        public void Start ()
        {
            indexOfTime = 0;
            if (TimesIsOver)
            {
                OnTimeIsOver ();
                return;
            }
            if (debug) Debug.Log ($"{("Start".a())} {this.ToString().warn()}", this);
            if (debug) Debug.Log (Bits.Log (), this);
            if (debug) Debug.Log (currentBit, this);
            currentBit = Bits.First ();
            if (debug) Debug.Log (currentBit, this);
            onStart.Invoke (currentBit);
        }

        public void UpdateInTime (float time)
        {
            if (TimesIsOver) return;

            if (currentBit == null || Bits.Length == 0) return; //??? ЧТо делать не заню с этим, вроде работает
            if (currentBit.StartTime <= time)
            {
                unityEvent.Invoke (currentBit);

                indexOfTime++;
                if (TimesIsOver)
                {
                    OnTimeIsOver ();
                    return;
                }
                try
                {
                    currentBit = Bits[indexOfTime];

                }
                catch (System.Exception) { }
            }
        }
        public override string ToString ()
        {
            return $"{Variable}";
        }

        [SerializeField] UnityEventIBitData TimeIsOver;
        public void OnTimeIsOver ()
        {
            if (debug) Debug.Log ($"{("TimesIsOver".err())} {this.ToString().warn()}", this);
            if (Bits.Count () > 0)
                TimeIsOver.Invoke (Variable.Value.Bits.Last ());
        }
    }
}
