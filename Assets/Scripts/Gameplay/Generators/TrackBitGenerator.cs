using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace CyberBeat
{
    public class TrackBitGenerator : BitGenerator<TrackBit>
    {
        [SerializeField] List<TrackBitItemData> bitTimeItems ;
        public override IEnumerable<BitTimeItem<TrackBit>> BitTimeItems => bitTimeItems;

        
    }
}
