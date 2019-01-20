using SonicBloom.Koreo;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CyberBeat
{
    public abstract class BitGenerator<TBitData> : TimeController
    where TBitData : IBitData
    {
        public abstract IEnumerable<BitTimeItem<TBitData>> BitTimeItems{get;}
        public override IEnumerable<ITimeUpdateable> TimeUpdateables { get { return BitTimeItems; } }
    }
}
