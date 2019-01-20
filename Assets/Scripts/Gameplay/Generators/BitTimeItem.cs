using GameCore;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CyberBeat
{
    public abstract class BitTimeItem<TBitData> : ATimeUpdateable<ABitDataCollectionVariable, ABitDataCollection, GameEventListenerIBitData.UnityEventIBitData, IBitData>
        where TBitData : IBitData
        {
            List<IBitData> Bits { get { return Variable.Value.Bits; } }
            public override IEnumerable<ITimeItem> TimeItems => Bits;
            public override ITimeItem CurrentTimeItem
            {
                get => currentBit;
                set => currentBit = value as IBitData;
            }
            IBitData currentBit;
            public override void UpdateInTime (float time)
            {
                if (TimesIsOver) return;

                if (currentBit.BitTime <= time)
                {
                    UnityEvent.Invoke (currentBit);

                    indexOfTime++;
                    if (TimesIsOver) return;
                    currentBit = Bits[indexOfTime];
                    // CurrentTimeItem = TimeItems.ElementAt(indexOfTime);
                }
            }
            public override string ToString ()
            {
                return $"{Variable}";
            }
        }
}
